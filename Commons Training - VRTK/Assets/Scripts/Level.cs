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

    //Boolean For Tasks in Levels
    //Level 1 
    public static bool level1Vest = false;                            //Works.... Collect Vest
    public static bool level1Round = false;                          //Works.... Fully complete Rounds
    public static bool level1Printer = false;                       //Works....Fill 2 Printers

    //Level 2
    public static bool level2USB = false;                            //Works.... Collect 1 USB
    public static bool level2Client = false;                        //Works.... Answer 1 MC Client
    public static bool level2Trash = false;                        //Works.... Collect 1 Trash of any kind

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

    // Start is called before the first frame update
    void Start()
    {
        startPosition = player.transform.position;
        countDownText = countDown.GetComponent<TextMeshProUGUI>();
        countDownText.enabled = true;
        levelCompletedText = levelCompleted.GetComponent<TextMeshProUGUI>();
        levelCompletedText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       /* Debug.Log("Level1Vest: " + level1Vest);
        Debug.Log("Level1Round: " + level1Round);
        Debug.Log("Level1Printer: " + level1Printer);
        Debug.Log("Level2USB: " + level2USB);
        Debug.Log("Level2Client: " + level2Client);
        Debug.Log("Level2Trash: " + level2Trash);
        Debug.Log("Level3Monitor: " + level3Monitor);
        Debug.Log("Level3ClientDesk: " + level3ClientDesk);
        Debug.Log("Level3ClientLab: " + level3ClientLab); */
        timer -= Time.deltaTime;
        WalkToDesk();
        CheckLevel();
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
        if (level1Vest && level1Printer && level1Round && level == 1)
        {
            StartCoroutine(DisplayLevelCompleted(5));
            level += 1;
        }
        if (level2Client && level2USB && level2Trash && level == 2)
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
    //functions for level 2
}
