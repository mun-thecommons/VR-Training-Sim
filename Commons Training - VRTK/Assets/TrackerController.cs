using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TrackerController : MonoBehaviour
{
    public GameObject[] taskList;
    private Canvas trackerCanvas;
    private int taskListIndex = 0;
    public GameObject HUD;
    private Canvas HUDCanvas;
    // Start is called before the first frame update
    void Start()
    {
        trackerCanvas = gameObject.GetComponent<Canvas>();
        HUDCanvas = HUD.GetComponent<Canvas>();
        taskList[0].GetComponent<Image>().color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (MasterController.inTracker)
        {
            ToggleTask();
        }
    }

    void ToggleTask()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight) || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft))
        {
            taskList[taskListIndex].GetComponent<Image>().color = Color.white;
            if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight))
            {
                taskListIndex = (taskListIndex + 1) <= (taskList.Length - 1) ? (taskListIndex + 1) : 0;
            }
            else
            {
                taskListIndex = taskListIndex - 1 >= 0 ? taskListIndex - 1 : taskList.Length - 1;
            }
            taskList[taskListIndex].GetComponent<Image>().color = Color.red;
        }
        else if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            trackerCanvas.enabled = false;
            MasterController.inTracker = false;
            HUDCanvas.enabled = true;
            MasterController.inMenu = true;
        }  
    }
}
