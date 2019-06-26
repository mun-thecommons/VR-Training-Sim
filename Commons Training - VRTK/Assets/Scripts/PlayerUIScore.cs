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
    public static int staplers = 0;
    public static Canvas mainCanvas;

    private int totalScore;
    private GameObject staplerShoot;
    private GameObject rightHand;

    void Start()
    {
        playerUIscore = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        mainCanvas = gameObject.GetComponent<Canvas>();
        mainCanvas.enabled = false;

        rightHand = GetRightHand();
    }

    void Update()
    {
        totalScore = QuestionInput.profScore + QuestionInput.profScore + QuestionInput.custServScore;
        playerUIscore.SetText("Pro: " +QuestionInput.profScore.ToString()+ "\nTech: " +QuestionInput.techScore.ToString()+ "\nC-Srv: "+ QuestionInput.custServScore.ToString() + "\ntotal: " +totalScore.ToString() + "\nstaplers: " + staplers.ToString());

        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            mainCanvas.enabled = !mainCanvas.enabled;
        }

        if (OVRInput.GetDown(OVRInput.RawButton.A))
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
            staplerShoot.GetComponent<Rigidbody>().AddForce(rightHand.transform.forward*100);
            staplers--;
        }
    }

    GameObject GetRightHand()
    {
        GameObject rightHand = GameObject.Find("RightHandAnchor");
        if (rightHand != null)
        {
            return rightHand;
        }
        else
        {
            Debug.Log("Can't find right hand");
            return null;
        }
    }
}


