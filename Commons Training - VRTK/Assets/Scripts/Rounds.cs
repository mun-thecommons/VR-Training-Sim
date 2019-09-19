﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rounds : MonoBehaviour {

    private TextMeshProUGUI output;
    private int seconds = 0;
    public int minutes = 0;
    private AudioSource roundsDoneSignal;
    static private GameObject[] roundsStations;

    private float waitTime = 1f;

    private float timer;

    static public bool roundsDone;

    void Start()
    {
        roundsStations = GameObject.FindGameObjectsWithTag("Station");
        output = gameObject.GetComponent<TextMeshProUGUI>();
        roundsDoneSignal = GetComponent<AudioSource>();
    }

    IEnumerator done()
    {
        roundsDoneSignal.Play();
        minutes = 0;
        seconds = 0;
        yield return new WaitForSeconds(5);
        foreach(GameObject r in roundsStations)
        {
            r.GetComponent<RoundsLight>().isDone = false;
            r.GetComponent<Renderer>().material.color = Color.red;
        }
 
    }

    void Update()
    {       
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

        if (roundsDone)
        {
            StartCoroutine(done());
            roundsDone = false;
        }


        if (minutes > 4)
        {
            output.text = "Rounds Needed Last Rounds  " + minutes.ToString() + ":" + seconds.ToString();
            output.fontSize = 27;
            output.color = Color.red;                                  //if Rounds are not done in the next 5 minutes game over???
           // QuestionInput.ScoreDecrement(2);  
          
        }
        else
            output.text = "Last Rounds " + minutes.ToString() + ":" + seconds.ToString();
    }
   
    public static void checkRounds()
    {
        foreach (GameObject r in roundsStations)
        {
            if (!r.GetComponent<RoundsLight>().isDone)
            {
                roundsDone = false;
                return;
            }
        }
        Debug.Log("All rounds done");
        roundsDone = true;
        if (Level.level == 2)
        {
            Level.level2Round = true;
        }
        MasterController.ScoreModify(1, 0, 0, true, false);
    }
}
