using System.IO;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;
using System.Text.RegularExpressions;




public class MasterController : MonoBehaviour
{
    public GameObject staplerPrefab;
    public Transform staplerParent;
    public GameObject rightHand;

    public static TextMeshProUGUI playerUIscore;
    public static TextMeshProUGUI scoreBreakDownText;
    public static TextMeshProUGUI totalScoreText;
    public static TextMeshProUGUI staplerCountText;
    public static TextMeshProUGUI coinCountText;
    public static TextMeshProUGUI mainFrameText;
    public static bool vestCollected = false;
    public static int staplers = 100;
    public static int coins = 0;
    public static Canvas mainCanvas;    
    public static Audio audio;

    private static int techScore = 0;
    private static int custServScore = 0;
    private static int profScore = 0;
    private static int totalScore = 0;
    private GameObject staplerShoot;
    public TextAsset instructions;
    public static List<string> instructionsArray = new List<string> { };
    private int instrArrayCounter = 0;
    public Button exitGameButton;
    public Button operationButton;

    void Start()
    {
        //playerUIscore = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        scoreBreakDownText = GameObject.Find("ScoreDetailedBox").GetComponentInChildren<TextMeshProUGUI>();
        totalScoreText = GameObject.Find("XPointsBox").GetComponentInChildren<TextMeshProUGUI>();
        staplerCountText = GameObject.Find("StaplerBox").GetComponentInChildren<TextMeshProUGUI>();
        mainFrameText = GameObject.Find("MainFrameBox").GetComponentInChildren<TextMeshProUGUI>();
        mainCanvas = gameObject.GetComponent<Canvas>();
        mainCanvas.enabled = false;
        audio = FindObjectOfType<Audio>();
        //playerUIscore.SetText("Pro: " + profScore.ToString() + "\nTech: " + techScore.ToString() + "\nC-Srv: " + custServScore.ToString() + "\ntotal: " + totalScore.ToString() + "\nstaplers: " + staplers.ToString());
        FileParse("instructions", instructions);
        exitGameButton = GameObject.FindGameObjectWithTag("ExitButton").GetComponent<Button>() as Button;
        operationButton = GameObject.FindGameObjectWithTag("OperationsButton").GetComponent<Button>() as Button;
    }

    void Update()
    {
        //view/hide UI canvas
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstick))
        {
            mainCanvas.enabled = !mainCanvas.enabled;
            mainFrameText.SetText("Accessing Main Frame...\n Press Left Controller X to scroll through the instructions manual");
            
        }
        //disables player's movements and allows to scroll through instrxns in the mainframe canvas
        if (mainCanvas.isActiveAndEnabled == true)
        {
            GameObject.Find("OVRPlayerController").GetComponent<OVRPlayerController>().enabled = false;

            if (OVRInput.GetDown(OVRInput.RawButton.Y))      
            {
                
            }
            if (OVRInput.GetDown(OVRInput.RawButton.X))

            {
                mainFrameText.SetText(instructionsArray[instrArrayCounter]);

                if (instrArrayCounter < instructionsArray.Count - 1)
                {
                    instrArrayCounter++;

                }
                else
                {
                    instrArrayCounter = 0;
                }
            }
        }
        else {
            GameObject.Find("OVRPlayerController").GetComponent<OVRPlayerController>().enabled = true;
        }
       

       
        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            ShootStapler();
        }
        
    
        

    }
    
    //reading instr from the file
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

    //shooting fxn
    void ShootStapler()
    {
        if(staplers > 0)
        {
            staplerShoot = Instantiate(staplerPrefab, rightHand.transform.position, rightHand.transform.rotation) as GameObject;
            staplerShoot.transform.parent = staplerParent;
            staplers--;
            // playerUIscore.SetText("Pro: " + profScore.ToString() + "\nTech: " + techScore.ToString() + "\nC-Srv: " + custServScore.ToString() + "\ntotal: " + totalScore.ToString() + "\nstaplers: " + staplers.ToString());
            staplerCountText.SetText(staplers.ToString());
        }
    }

    //score modifier fxn
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

        //coinCountText.SetText(coins.ToString());
        scoreBreakDownText.SetText("Pro: " + profScore.ToString() + "\nTech: " + techScore.ToString() + "\nC-Srv: " + custServScore.ToString());
        totalScoreText.SetText(totalScore.ToString());

        //playerUIscore.SetText("Pro: " + profScore.ToString() + "\nTech: " + techScore.ToString() + "\nC-Srv: " + custServScore.ToString() + "\ntotal: " + totalScore.ToString() + "\nstaplers: " + staplers.ToString());
    }
}


