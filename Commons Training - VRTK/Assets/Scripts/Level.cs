using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
    //Player and Start Position
    public GameObject player;
    private Vector3 startPosition;

    //Integer 
    public static int level = 1;
    public TextMeshProUGUI mainFrameText;
    
    //Boolean For Tasks in Levels
    //Level 1 
    public static bool level1Vest = false;                            //Works.... Collect Vest
    public static bool level1USB = false;                          //Works.... Fully complete Rounds
    public static bool level1Printer = false;                       //Works....Fill 2 Printers

    //Level 2
    public static bool level2Round = false;                            //Works.... Collect 1 USB
    public static bool level2Client = false;                        //Works.... Answer 1 MC Client
    public static bool level2Trash = false;                        //Works.... Collect 1 Trash of any kind
    public static bool level2Cash = false;                        //Works.... Collect 1 Trash of any kind

    //Level 3
    public static bool level3Monitor = false;                        //Works.... Fix 1 Monitor
    public static bool level3ClientDesk = false;                    //Works.... Answer All questions (Don't need to be correct)
    public static bool level3ClientLab = false;                    //Works.... Answer 2 Questions Correctly

    //Level 4

    //Timer 
    public float timer = 30f;
    public float resetTimer = 30f;

    //Count Down Timer Canvas
    public GameObject countDown;
    public GameObject levelCompleted;
    private TextMeshProUGUI countDownText;
    private TextMeshProUGUI levelCompletedText;

    private GameObject robbie;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = player.transform.position;
        countDownText = countDown.GetComponent<TextMeshProUGUI>();
        levelCompletedText = levelCompleted.GetComponent<TextMeshProUGUI>();
        levelCompletedText.enabled = false;
        robbie = GameObject.FindGameObjectWithTag("Robbie");
    }

    // Update is called once per frame
    void Update()
    {
        if (robbie.GetComponent<RobotController>().tutorialFinished)
        {
            if(MasterController.vestCollected == false)
            {
                countDownText.enabled = true;
            }
            timer -= Time.deltaTime;
            WalkToDesk();
            CheckLevel();
        }
    }

    //Display Level Complete Message
    IEnumerator DisplayLevelCompleted(float time)
    {
        levelCompletedText.enabled = true;
        levelCompletedText.text = "Level : " + level + " Complete";
        yield return new WaitForSeconds(time);
        levelCompletedText.enabled = false;
    }

    //functions for level 1
    private void WalkToDesk()
    {
        countDownText.text = "Go to CS Desk: " + timer.ToString("F2");
        if (timer <= 0f && !MasterController.vestCollected && !MasterController.inMenu)
        {
            countDownText.text = "Out of time";
            if (timer <= -3f)
            {
                player.transform.position = startPosition;
                timer = resetTimer;
            }
        }
        else if (MasterController.vestCollected && timer >= -3f)
        {
            countDownText.enabled = false;
        }

    }

    private void CheckLevel()
    {
        SetMainframeText();
        if (level1Vest && level1Printer && level1USB && level == 1)
        {
            StartCoroutine(DisplayLevelCompleted(5));
            level += 1;
        }
        if (level2Client && level2Round && level2Trash && level2Cash && level == 2)
        {
            StartCoroutine(DisplayLevelCompleted(5));
            level += 1;
        }
        if (level3Monitor && level3ClientDesk && level3ClientLab && level == 3)
        {
            StartCoroutine(DisplayLevelCompleted(5));
            level += 1;
        }
    }

    private void SetMainframeText()
    {
        if(level == 1)
        {
            mainFrameText.text = "Level: " + Level.level + "\n Get vest: " + (level1Vest ? "Complete" : "Incomplete")
                + "\n Fill printers: " + (level1Printer ? "Complete" : "Incomplete")
                + "\n Return USB: " + (level1USB ? "Complete" : "Incomplete");
        }
        else if(level == 2)
        {
            mainFrameText.text = "Level: " + Level.level + "\n Help a client in the lab: " + (level2Client ? "Complete" : "Incomplete")
                   + "\n Complete rounds: " + (level2Round ? "Complete" : "Incomplete")
                   + "\n Pick up garbage: " + (level2Trash ? "Complete" : "Incomplete")
                   + "\n Help a Client with their Campus Card: " + (level2Cash ? "Complete" : "Incomplete");
        }
        else if (level == 3)
        {
            mainFrameText.text = "Level: " + Level.level + "\n Fix a monitor: " + (level3Monitor ? "Complete" : "Incomplete")
                   + "\n Help a client at the desk: " + (level3ClientDesk ? "Complete" : "Incomplete")
                   + "\n Help more clients in the lab: " + (level3ClientLab ? "Complete" : "Incomplete");
        }
    }
    
}
