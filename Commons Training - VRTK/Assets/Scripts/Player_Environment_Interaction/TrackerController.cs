﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TrackerController : MonoBehaviour
{
    public TextMeshProUGUI countTaskA;
    public TextMeshProUGUI countTaskB;
    public TextMeshProUGUI countTaskC;
    public GameObject[] trackerUpdate;
    private Canvas trackerCanvas;
    private int taskListIndex = 0;
    public GameObject HUD;

    // Start is called before the first frame update
    void Start()
    {
        SetTrackerStats();
        trackerCanvas = gameObject.GetComponent<Canvas>();
        trackerCanvas.enabled = false;
        trackerUpdate[0].GetComponent<Image>().color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        ToggleTask();
    }

    void ToggleTask()
    {
        if (!MasterController.inMenu && MasterController.inTracker)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown)||OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp)||OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight)||OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft))
            {
                trackerUpdate[taskListIndex].GetComponent<Image>().color = Color.white;
                /*
                if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown))
                {
                    taskListIndex = (taskListIndex + 1) <= (trackerUpdate.Length - 1) ? (taskListIndex + 1) : 0;
                }
                else
                {
                    taskListIndex = taskListIndex - 1 >= 0 ? taskListIndex - 1 : trackerUpdate.Length - 1;
                }
                */
                if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight)|| OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft))
                {
                    taskListIndex = (taskListIndex % 2 == 0) ? (taskListIndex + 1) : (taskListIndex-1);
                }
                if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp))
                {
                    taskListIndex = ((taskListIndex == 0) || (taskListIndex == 1)) ? (taskListIndex+6) : (taskListIndex-2);
                }
                if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown))
                {
                    taskListIndex = ((taskListIndex == trackerUpdate.Length-1)|| (taskListIndex == trackerUpdate.Length - 2)) ? (taskListIndex-6) : (taskListIndex+2);
                }
                
                trackerUpdate[taskListIndex].GetComponent<Image>().color = Color.red;
            }
            else if (OVRInput.GetDown(OVRInput.RawButton.X))
            {
                if (taskListIndex == trackerUpdate.Length - 1 || taskListIndex == trackerUpdate.Length - 2)
                {
                    ExitTracker();

                }
                else
                {
                    switch (taskListIndex)
                    {
                        case 0:
                            MasterController.trackerTaskA++;
                            break;
                        case 1:
                            MasterController.trackerTaskB++;
                            break;
                        case 2:
                            MasterController.trackerTaskC++;
                            break;
                    }
                    SetTrackerStats();
                    //ExitTracker();
                }
            }
        }
    }

    void SetTrackerStats()
    {
        countTaskA.SetText(MasterController.trackerTaskA.ToString());
        countTaskB.SetText(MasterController.trackerTaskB.ToString());
        countTaskC.SetText(MasterController.trackerTaskC.ToString());
    }

    void ExitTracker()
    {
        
        //Turn off Tracker Canvas
        trackerCanvas.enabled = false;
        MasterController.inTracker = false;
        //turn on HUD Canvas
        
        //Enable movement
        MasterController.EnableMovement();
    } 

}
