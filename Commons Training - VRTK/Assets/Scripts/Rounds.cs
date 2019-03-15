using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rounds : MonoBehaviour {
   

    private TextMeshProUGUI output;
    private int seconds = 0;
    public int minutes = 0;
    private AudioSource roundsDoneSignal;

    private float waitTime = 1f;

    private float timer;

    public bool roundsDone;

    void Start()
    {
        output = gameObject.GetComponent<TextMeshProUGUI>();
        roundsDoneSignal = GetComponent<AudioSource>();
    }

    IEnumerator done()
    {
        if (roundsDone)
        {
            roundsDoneSignal.Play();
            minutes = 0;
            seconds = 0;
            yield return new WaitForSeconds(5);
            RoundsCard.isRound1Done = false;
            RoundsCard.isRound2Done = false;
            RoundsCard.isRound3Done = false;
            RoundsCard.isRound4Done = false;

            /* TouchDetection.station1 = false;
             TouchDetection.station2 = false;
             TouchDetection.station3 = false;
             TouchDetection.station4 = false;
             */
        }  
    }

    void Update()
    {
       
       //  roundsDone = TouchDetection.station1 && TouchDetection.station2 && TouchDetection.station3 && TouchDetection.station4; 

        roundsDone = RoundsCard.isRound1Done && RoundsCard.isRound2Done && RoundsCard.isRound3Done && RoundsCard.isRound4Done;
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            seconds += 1;

            if(seconds > 59)
            {
                minutes += 1;
                seconds = 0;
            }
            timer = 0f;
        }

        StartCoroutine(done());

        if (minutes > 4)
        {
            output.text = "Rounds Needed Last Rounds  " + minutes.ToString() + ":" + seconds.ToString();
            output.fontSize = 27;
            output.color = Color.red;
        }
        else
            output.text = "Last Rounds " + minutes.ToString() + ":" + seconds.ToString();
    }
   
}
