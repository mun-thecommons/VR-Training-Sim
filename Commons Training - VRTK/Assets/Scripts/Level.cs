using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
    //Player and Start Position
    public GameObject player;
    public Transform startPosition;

    //Integer 
    private int level = 0;



    //Boolean For Tasks in Levels
    //Level 1
    public static bool level1Round = false;
    public static bool level1Printer = false;

    //Level 2
    public static bool level2USB = false;
    public static bool level2Client = false;

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
        countDownText = countDown.GetComponent<TextMeshProUGUI>();
        countDownText.enabled = true;
        levelCompletedText = levelCompleted.GetComponent<TextMeshProUGUI>();
        levelCompletedText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //count down timer
        timer -= Time.deltaTime;

        //Complete Level 1
        WalkToDesk();
        if(MasterController.vestCollected && level1Printer && /*level1Round &&*/ level == 0)
        {
            level += 1;
            StartCoroutine(DisplayLevelCompleted(5)); 
        }

        if(level2Client && level2USB && level == 1)
        {
            level += 1;
            StartCoroutine(DisplayLevelCompleted(5));
        }

        //Complete Level 2

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
        if (timer <= 0f && !MasterController.vestCollected)
        {
            countDownText.text = "Out of time";
            if (timer <= -3f)
            {
                player.transform.position = startPosition.transform.position;
                timer = resetTimer;
            }
        }
        else if (MasterController.vestCollected && timer >= 0f)
        {
            countDownText.enabled = false;
        }
    }

    

    //functions for level 2
}
