using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rounds : MonoBehaviour {

    private TextMeshProUGUI output;

    private int seconds = 0;
    public int minutes = 0;

    private float waitTime = 1f;

    private float timer;

    public bool roundsDone;

    void Start()
    {
        output = gameObject.GetComponent<TextMeshProUGUI>();
    }

    IEnumerator done()
    {
        if (roundsDone)
        {
            minutes = 0;
            seconds = 0;
            yield return new WaitForSeconds(5);
            TouchDetection.station1 = false;
            TouchDetection.station2 = false;
            TouchDetection.station3 = false;
            TouchDetection.station4 = false;
        }  
    }

    void Update()
    {
        roundsDone = TouchDetection.station1 && TouchDetection.station2 && TouchDetection.station3 && TouchDetection.station4; 

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
