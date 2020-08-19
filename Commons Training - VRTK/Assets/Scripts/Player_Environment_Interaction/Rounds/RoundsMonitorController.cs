//import libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ##Script Description
/// This script controls the display of the remaining time to complete round
/// When the elapsed time from the previous round exceeds a preset value, the user is warned to complete rounds as needed
/// </summary>
public class RoundsMonitorController : MonoBehaviour
{
    //rounds Timer
    public Text roundsTimerText;
    //Monitor Controller
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
        if (monitorControllerScript.isBroken)
        {
            roundsTimerText.text = "";
        }
        else
        {
            // Prettefy the last rounds timer to the format mm:ss
            int timeSeconds = Mathf.RoundToInt(time);

            int minutes = timeSeconds / 60;

            string secondsStr = (timeSeconds % 60 < 10) ? ("0" + (timeSeconds % 60).ToString()) : (timeSeconds % 60).ToString();
            string timeStr = minutes.ToString() + ":" + secondsStr;

            if (time <= RoundsController.roundsInterval)
            {
                roundsTimerText.text = "      Last\nRounds: " + timeStr;

                // Green
                roundsTimerText.color = new Color(0.14f, 0.85f, 0.15f);
            }
            else
            {
                roundsTimerText.text = "      Rounds\nNeeded! " + timeStr;

                roundsTimerText.color = Color.red;
            }

        }

    }

}
