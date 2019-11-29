using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorError : MonoBehaviour
{
    public GameObject[] monitors;
    public float minWait, maxWait;

    private float timer;
    private float timeToWait;

    private void Start()
    {

        monitors = GameObject.FindGameObjectsWithTag("Monitor");
        timer = 0.0f;
        timeToWait = Random.Range(minWait, maxWait);
    }

    private void Update()
    {
        if (timer >= timeToWait)
        {
            int index = Random.Range(0, monitors.Length - 1);
            monitors[index].GetComponent<MonitorController>().Break();

            timer = 0.0f;
            timeToWait = Random.Range(minWait, maxWait);
        }

        timer += Time.deltaTime;
    }
}
