using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;


public class PlayerUIScore : MonoBehaviour
{
    TextMeshProUGUI playerUIscore;
    public static Canvas mainCanvas;
    int totalScore;


    void Start()
    {
        playerUIscore = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        mainCanvas = gameObject.GetComponent<Canvas>();
        mainCanvas.enabled = false;
        
    }

    void Update()
    {
        totalScore = QuestionInput.profScore + QuestionInput.profScore + QuestionInput.custServScore;
        playerUIscore.SetText("Pro: " +QuestionInput.profScore.ToString()+ "\nTech: " +QuestionInput.techScore.ToString()+ "\nC-Srv: "+ QuestionInput.custServScore.ToString() + "\ntotal: " +totalScore.ToString() );

        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            mainCanvas.enabled = !mainCanvas.enabled;
        }        
    }
}

