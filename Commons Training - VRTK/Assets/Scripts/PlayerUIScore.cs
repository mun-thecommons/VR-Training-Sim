using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;



public class PlayerUIScore : MonoBehaviour
{
     TextMeshProUGUI playerUIscore;
     static Canvas mainCanvas;


    void Start()
    {

        playerUIscore = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        mainCanvas = gameObject.GetComponent<Canvas>();
        mainCanvas.enabled = false;
        
    }

    void Update()
    {
    
        playerUIscore.SetText("Score:  "+ QuestionInput.totalScore.ToString());
       
    }

    
    static public void TurnScoreOff()
    {
        mainCanvas.enabled = false;
        
    }

    static public void TurnScoreOn()
    {
        mainCanvas.enabled = true;
        Debug.Log("showing score");
    }
}


