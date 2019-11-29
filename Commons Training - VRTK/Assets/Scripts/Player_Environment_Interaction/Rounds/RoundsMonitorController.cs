using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsMonitorController : MonoBehaviour
{

    public Text roundsTimerText;

    private MonitorController monitorControllerScript;


    // Start is called before the first frame update
    void Start()
    {
        monitorControllerScript = gameObject.GetComponent<MonitorController>();
        UpdateTime(0f);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime(RoundsController.lastRounds);
    }

    private void UpdateTime(float time)
    {
        Debug.Log(monitorControllerScript.isBroken.ToString());
        if (monitorControllerScript.isBroken)
        {
            roundsTimerText.text = "";
        }
        else
        {
            if (time <= RoundsController.roundsInterval)
            {
                int timeSeconds = Mathf.RoundToInt(time);

                int minutes = timeSeconds / 60;

                string secondsStr = (timeSeconds % 60 < 10) ? ("0" + (timeSeconds % 60).ToString()) : (timeSeconds % 60).ToString();
                string timeStr = minutes.ToString() + ":" + secondsStr;

                roundsTimerText.text = "      Last\nRounds: " + timeStr;
            }
            else
            {
                roundsTimerText.text = "      Rounds\nNeeded!";
            }

        }

    }

}
