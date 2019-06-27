using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;


public class PlayerUIScore : MonoBehaviour
{
    TextMeshProUGUI playerUIscore;

    public GameObject staplerPrefab;
    public Transform staplerShootParent;

    //Make sure to change the staplers back to 0 after testing
    public static int staplers = 100;
    public static Canvas mainCanvas;
    public GameObject rightHand;

    private int totalScore;
    private GameObject staplerShoot;

    void Start()
    {
        playerUIscore = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        mainCanvas = gameObject.GetComponent<Canvas>();
        mainCanvas.enabled = false;
    }

    void Update()
    {
        totalScore = QuestionInput.profScore + QuestionInput.profScore + QuestionInput.custServScore;
        playerUIscore.SetText("Pro: " +QuestionInput.profScore.ToString()+ "\nTech: " +QuestionInput.techScore.ToString()+ "\nC-Srv: "+ QuestionInput.custServScore.ToString() + "\ntotal: " +totalScore.ToString() + "\nstaplers: " + staplers.ToString());

        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstick))
        {
            mainCanvas.enabled = !mainCanvas.enabled;
        }

        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            ShootStapler();
        }
    }
    
    void ShootStapler()
    {
        if(staplers > 0)
        {
            staplerShoot = Instantiate(staplerPrefab, rightHand.transform.position, rightHand.transform.rotation) as GameObject;
            staplerShoot.transform.parent = staplerShootParent;
            staplers--;
        }
    }

}


