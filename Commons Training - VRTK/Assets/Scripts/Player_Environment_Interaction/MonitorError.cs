using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//import libraries
/// <summary>
/// ##Script Description
///  This script manages to break one random computer every a random preset amount of time 
/// </summary>
public class MonitorError : MonoBehaviour
{
    public Sprite monitorOffSprite;

    public GameObject[] monitors;
    public static GameObject[] monitorArr;

    public float minWait, maxWait;

    private float timer;
    private float timeToWait;

    private bool lastMonitorState;

    private void Start()
    {
        monitors = GameObject.FindGameObjectsWithTag("Monitor");
        monitorArr = monitors;

        timer = 0.0f;
        timeToWait = Random.Range(minWait, maxWait);

        lastMonitorState = !GameManager.monitorsOn;
    }

    private void Update()
    {
        if (lastMonitorState != GameManager.monitorsOn)
        {
            foreach (GameObject monitor in monitors)
            {
                monitor.GetComponent<MonitorController>().monitorImg.enabled = GameManager.monitorsOn;
            }
            lastMonitorState = GameManager.monitorsOn;
        }

        if (timer >= timeToWait)
        {
            int index = Random.Range(0, monitors.Length - 1);
            monitors[index].GetComponent<MonitorController>().Break();

            timer = 0.0f;
            timeToWait = Random.Range(minWait, maxWait);
        }

        timer += Time.deltaTime;
    }
    
    public static void breakMonitors(int number) // Break some random number of monitors
    {
        for (int i = 0; i < number; i++)
        {
            int index = Random.Range(0, MonitorError.monitorArr.Length - 1);
            MonitorError.monitorArr[index].GetComponent<MonitorController>().Break();
        }
    }
}
