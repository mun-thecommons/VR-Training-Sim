using System.Collections;
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
            if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown) || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp))
            {
                trackerUpdate[taskListIndex].GetComponent<Image>().color = Color.white;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown))
                {
                    taskListIndex = (taskListIndex + 1) <= (trackerUpdate.Length - 1) ? (taskListIndex + 1) : 0;
                }
                else
                {
                    taskListIndex = taskListIndex - 1 >= 0 ? taskListIndex - 1 : trackerUpdate.Length - 1;
                }
                trackerUpdate[taskListIndex].GetComponent<Image>().color = Color.red;
            }
            else if (OVRInput.GetDown(OVRInput.RawButton.X))
            {
                if (taskListIndex == trackerUpdate.Length - 1)
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
