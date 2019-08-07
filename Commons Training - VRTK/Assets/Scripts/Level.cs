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

    //Level Completion Booleans
    private bool level1Completed = false;
    private bool level2Completed = false;
    private bool level3Completed = false;
    private bool level4Completed = false;
    private bool level5Completed = false;

    //Level1 Booleans
    //vestCollected remains true after the vest is collected
    private bool level1Round = false;
    private bool level1Printer = false;

    //Timer 
    public float timer = 30f;
    public float resetTimer = 30f;

    //Count Down Timer Canvas
    public GameObject countDown;
    private TextMeshProUGUI countDownText;

    // Start is called before the first frame update
    void Start()
    {
        countDownText = countDown.GetComponent<TextMeshProUGUI>();
        countDownText.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //count down timer
        timer -= Time.deltaTime;

        //Complete Level 1
        WalkToDesk();
        RoundAndPrinter();
        if(MasterController.vestCollected /*&& level1Round*/ &&  level1Printer)
        {
            level1Completed = true;
            Debug.Log(level1Completed);
            //StartCoroutine(DisplayLevelCompleted(5f));
        }

        //Complete Level 2
    }

    //Display Level Complete Message
    IEnumerator DisplayLevelCompleted(float time)
    {
        countDownText.enabled = true;
        countDownText.text = "Complete Level 1";
        yield return new WaitForSeconds(time);
        countDownText.enabled = false;
        
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

    private void RoundAndPrinter()
    {
        if (Rounds.roundsDone)
        {
            level1Round = true;
        }
        if (PrinterController.numOfPrintersFilled >= 2)
        {
            level1Printer = true;
        }
    }

    //functions for level 2
}
