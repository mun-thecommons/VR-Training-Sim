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
    public OVRPlayerController playerController;

    public static TextMeshProUGUI playerUIscore;
    public static TextMeshProUGUI scoreBreakDownText;
    public static TextMeshProUGUI totalScoreText;
    public static TextMeshProUGUI staplerCountText;
    public static TextMeshProUGUI coinCountText;
    [SerializeField] private static TextMeshProUGUI mainFrameText;
    public static bool vestCollected = false;
    public static bool inMenu = false;
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
    private int uiMenuCounter = 0;
    public GameObject exitGameButton;
    public GameObject operationButton;
    static private GameObject[] uiMenuOptionsArray;
    private GameObject player;
    private IEnumerator accessMainframe;
    public int numOfDots; //mainFrameText Coroutine

    private void Start()
    {
        staplerCountText = GameObject.FindGameObjectWithTag("StaplerCount").GetComponent<TextMeshProUGUI>();
        coinCountText = GameObject.FindGameObjectWithTag("CoinCount").GetComponent<TextMeshProUGUI>();

        //playerUIscore = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        scoreBreakDownText = GameObject.Find("ScoreDetailedBox").GetComponentInChildren<TextMeshProUGUI>();
        totalScoreText = GameObject.Find("XPointsBox").GetComponentInChildren<TextMeshProUGUI>();
        
        mainFrameText = GameObject.Find("MainFrameBox").GetComponentInChildren<TextMeshProUGUI>();
        playerController = GameObject.Find("OVRPlayerController").GetComponent<OVRPlayerController>();
        mainCanvas = gameObject.GetComponent<Canvas>();
        mainCanvas.enabled = false;
        audio = GameObject.Find("LocalAvatar").GetComponent<Audio>();
        //playerUIscore.SetText("Pro: " + profScore.ToString() + "\nTech: " + techScore.ToString() + "\nC-Srv: " + custServScore.ToString() + "\ntotal: " + totalScore.ToString() + "\nstaplers: " + staplers.ToString());
        FileParse("instructions", instructions);
        exitGameButton = GameObject.Find("ExitButton");
        operationButton = GameObject.Find("OperationsManualButton");
        uiMenuOptionsArray = GameObject.FindGameObjectsWithTag("UIMenuOption");
        //Debug.Log(uiMenuOptionsArray.Length);
        player = GameObject.Find("OVRPlayerController");
    }

    void Update()
    {
        DisplayInventory();
        TakeInput();
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

    void TakeInput()
    {
        //view/hide UI canvas
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstick))
        {
            if (!inMenu)
            {
                mainCanvas.enabled = true;
                playerController.enabled = false;
                inMenu = true;
                accessMainframe = AccessMainframe();
                StartCoroutine(accessMainframe);
            }
            else if (mainCanvas.isActiveAndEnabled)
            {
                mainCanvas.enabled = false;
                playerController.enabled = true;
                inMenu = false;
                StopCoroutine(accessMainframe);
            }
        }
        //disables player's movements and allows to scroll through instrxns in the mainframe canvas
        if (mainCanvas.isActiveAndEnabled)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp) || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown))
            {
                uiMenuOptionsArray[uiMenuCounter].GetComponent<Image>().color = Color.red;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp))
                {
                    uiMenuCounter = uiMenuCounter + 1 <= uiMenuOptionsArray.Length - 1 ? uiMenuCounter + 1 : 0;
                }
                else
                {
                    uiMenuCounter = uiMenuCounter - 1 >= 0 ? uiMenuCounter - 1 : uiMenuOptionsArray.Length - 1;
                }
                uiMenuOptionsArray[uiMenuCounter].GetComponent<Image>().color = Color.grey;
            }
            else if (OVRInput.GetDown(OVRInput.RawButton.X))
            {
                if (uiMenuCounter == 0)
                {
                    StopCoroutine(accessMainframe);
                    mainFrameText.SetText(instructionsArray[instrArrayCounter]);
                    instrArrayCounter = instrArrayCounter < instructionsArray.Count - 1 ? instrArrayCounter + 1 : 0;
                }
                else
                {
                    mainFrameText.SetText("Exiting system...");
                    Debug.Log("Exiting game!!");
                    Application.Quit();
                }
            }
        }
        else if (OVRInput.GetDown(OVRInput.RawButton.B) && !inMenu)
        {
            ShootStapler();
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

    private void DisplayInventory()
    {
        staplerCountText.SetText(staplers.ToString());
        coinCountText.SetText(coins.ToString());
    }
}


