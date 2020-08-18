using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

/// <summary>
/// This script contains a lot of the player housed elements (Collectibles, Main Menu, etc.)
/// 
/// ##Detailed
/// This script is attached to the player character and holds functions for modifying the players score and accessing the 
/// MainMenu. 
/// 
/// </summary>
public class MasterController : MonoBehaviour
{
    public static GameObject teleportFunction;  /*!< @brief Associated with the Player Teleport mechanic*/
    public static double labSatisfaction = 1000.0;  /*!< @brief Player begins with a score of 1000*/
    public GameObject rightHand;
    public static OVRPlayerController playerController;

    public static TextMeshProUGUI scoreBreakDownText;
    public static TextMeshProUGUI totalScoreText;
    public static TextMeshProUGUI staplerCountText;
    public static TextMeshProUGUI coinCountText;
    public static TextMeshProUGUI baseTrashCount;
    public static TextMeshProUGUI metalTrashCount;
    public static TextMeshProUGUI plasticTrashCount;
    public static TextMeshProUGUI labSatisfactionText;
    public static TextMeshProUGUI scrapPaperText;

    [SerializeField] private static TextMeshProUGUI mainFrameText;
    public static bool vestCollected = false;
    public static bool inMenu = false;
    public static bool inTracker = false;
    public static int coins = 0;
    public static int baseTrash = 0;
    public static int metalTrash = 0;
    public static int plasticTrash = 0;
    public static int scrapPaper = 0;

    //tracker variables
    public static int trackerTaskA = 0;
    public static int trackerTaskB = 0;
    public static int trackerTaskC = 0;


    public static Canvas mainCanvas;
    public GameObject tracker;
    public static Audio audio;

    private static int techScore = 0;
    private static int custServScore = 0;
    private static int profScore = 0;

    private static int totalScore = 0;
    private GameObject staplerShoot;
    public TextAsset instructions;
    public static List<string> instructionsArray = new List<string> { };
    private int instrArrayCounter = 0;
    private int uiMenuCounter = 0;
    public GameObject exitGameButton;
    public GameObject operationButton;
    public GameObject[] uiMenuOptionsArray;
    public GameObject player;
    private IEnumerator accessMainframe;
    public int numOfDots; /*!< @brief Used as a **Loading Screen** for main Menu functionality */
    private Canvas trackerCanvas;   /*!< @brief Used for the Tracker functionality */

    /******************
     * Upon Start the GameObject variables will find their associated Gameobjects
     * 
     * **************/
    private void Start()
    {
        teleportFunction = GameObject.FindGameObjectWithTag("Teleportation");
        staplerCountText = GameObject.FindGameObjectWithTag("StaplerCount").GetComponent<TextMeshProUGUI>();
        coinCountText = GameObject.FindGameObjectWithTag("CoinCount").GetComponent<TextMeshProUGUI>();
        scoreBreakDownText = GameObject.Find("ScoreDetailedBox").GetComponentInChildren<TextMeshProUGUI>();
        totalScoreText = GameObject.Find("XPointsBox").GetComponentInChildren<TextMeshProUGUI>();
        mainFrameText = GameObject.Find("MainFrameBox").GetComponentInChildren<TextMeshProUGUI>();
        playerController = player.GetComponent<OVRPlayerController>();
        mainCanvas = gameObject.GetComponent<Canvas>();
        mainCanvas.enabled = false;
        audio = GameObject.Find("LocalAvatar").GetComponent<Audio>();
        FileParse("instructions", instructions);

        baseTrashCount = GameObject.FindGameObjectWithTag("BaseTrashCount").GetComponent<TextMeshProUGUI>();
        metalTrashCount = GameObject.FindGameObjectWithTag("MetalTrashCount").GetComponent<TextMeshProUGUI>();
        plasticTrashCount = GameObject.FindGameObjectWithTag("PlasticTrashCount").GetComponent<TextMeshProUGUI>();
        labSatisfactionText = GameObject.FindGameObjectWithTag("LabSatisfactionScore").GetComponent<TextMeshProUGUI>();
        scrapPaperText = GameObject.FindGameObjectWithTag("ScrapPaperCount").GetComponent<TextMeshProUGUI>();

        trackerCanvas = tracker.GetComponent<Canvas>();
        trackerCanvas.enabled = false;

        uiMenuOptionsArray[uiMenuCounter].GetComponent<Image>().color = Color.red;
    }

    void Update()
    {
        labSatisfaction -= 20*Time.deltaTime*CollectibleManager.numOfTrash;                      //Lab satisfaction due to trash
      
        DisplayInventory();
        DisplayTrashCount();
        TakeInput();
    }

    /*******************
     * Parses the "Instructions" Textfile so that it can be seen from within the Main menu
     * 
     * @note Unsure if this works correctly, This was left from Anush's work 
     * ****************/
    void FileParse(string toParse, TextAsset textFile)
    {
        string[] fLines = textFile.text.Split("\n"[0]);
        if (toParse.Equals("instructions"))
        {
            foreach (string line in fLines)
            {
                instructionsArray.Add(line);

            }
        }
    }
    /*********************
     * Takes the input from the Player for the UI options
     * 
     * ##Detailed
     * 
     * ******************/
    void TakeInput()
    {
        //view/hide UI canvas
        if (InputHandler.menuButton.state)
        {
            if (!inMenu && !inTracker)
            {
                mainCanvas.enabled = true;
                InputHandler.DisableMovement();
                inMenu = true;
            }
            else if (mainCanvas.isActiveAndEnabled)
            {
                mainCanvas.enabled = false;
                InputHandler.EnableMovement();
                inMenu = false;
               
            }
        }
        //disables player's movements and allows to scroll through instrxns in the mainframe canvas
        if (mainCanvas.isActiveAndEnabled)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight) || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft))
            {
                uiMenuOptionsArray[uiMenuCounter].GetComponent<Image>().color = Color.white;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight))
                {
                    uiMenuCounter = (uiMenuCounter + 1) <= (uiMenuOptionsArray.Length - 1) ? (uiMenuCounter + 1) : 0;
                }
                else
                {
                    uiMenuCounter = uiMenuCounter - 1 >= 0 ? uiMenuCounter - 1 : uiMenuOptionsArray.Length - 1;
                }
                uiMenuOptionsArray[uiMenuCounter].GetComponent<Image>().color = Color.red;
            }
            else if (InputHandler.selectButton.isDown)
            {
                if (uiMenuCounter == 0)
                {
                    //mainFrameText.SetText(instructionsArray[instrArrayCounter]);
                    instrArrayCounter = instrArrayCounter < instructionsArray.Count - 1 ? instrArrayCounter + 1 : 0;
                }
                else if (uiMenuCounter == 1)
                {
                    mainCanvas.enabled = false;
                    inMenu = false;
                    trackerCanvas.enabled = true;
                    inTracker = true;
                    InputHandler.DisableMovement();
                    //Turn on the Tracker Canvas
                }
                else if (uiMenuCounter == 2)
                {
                    //SceneManager.LoadScene("SampleScene");
                }
                else
                {
                    // UnityEditor.EditorApplication.isPlaying = false;
                    Application.Quit();
                }
            }
        }
    }


    /**********************************
     * Function used to increase or decrease the Player's score
     * 
     * ##Detailed
     * This function is what governs the points gained/lost in 3 categories. Those categories are *Professionalism* , *Customer Service*,
     * and *Technical Skills*.
     * 
     * @param prof This variable represents the points gained or lost in the "Profesionalism" category
     * @param cs This variable represents the points gained or lost in the "Customer Service" category
     * @param tech This variable represents the points gained or lost in the "Technical Skills" category
     * @param correct This variable signifies whether the character completed the task correctly 
     * @param playSound This variable signifies if there should be an audioclip played for the action
     * 
     * 
     * ********************************/
    public static void ScoreModify(int prof, int cs, int tech, bool correct, bool playSound)
    {
        profScore += prof;
        custServScore += cs;
        techScore += tech;
        totalScore = profScore + custServScore + techScore;
        if (playSound)
        {
            if (correct)
            {
                audio.correctSound();
            }
            else
            {
                audio.wrongSound();
            }
        }

        scoreBreakDownText.SetText("Pro: " + profScore.ToString() + "\nTech: " + techScore.ToString() + "\nC-Srv: " + custServScore.ToString());
        totalScoreText.SetText(totalScore.ToString());
    }

    /*******************
     * **Loading Screen** for the player while waiting for Main Menu to open
     * 
     * ****************/
    IEnumerator AccessMainframe()
    {
        string text = "Accessing Mainframe.";
        for(int i=0; i<=numOfDots; i++)
        {
            mainFrameText.SetText(text);
            yield return new WaitForSeconds(1f);
            text = text + ".";
        }
    }
    /*******************************
     * This functions displays the inventory of the player
     * 
     * @note This function may still need improvement??
     * 
     * **************************/
    private void DisplayInventory()
    {
        staplerCountText.SetText(StaplerProjectile.staplers.ToString());
        coinCountText.SetText(coins.ToString());
        scrapPaperText.SetText(scrapPaper.ToString());
    }
    /***************************
     * This function displays the amount of trash of each category the player has collected.
     * 
     * ##Detailed 
     * Thgis script will display how much trash of each category has been collected by the player. It will also display 
     * the lab satisfaction score.
     * 
     * ***********************/
    private void DisplayTrashCount()
    {
        baseTrashCount.SetText(baseTrash.ToString());
        metalTrashCount.SetText(metalTrash.ToString());
        plasticTrashCount.SetText(plasticTrash.ToString());
        double roundLabSat = Math.Round(labSatisfaction * 100.0) / 100;
        labSatisfactionText.SetText(roundLabSat.ToString());
    }

}


