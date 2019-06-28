﻿using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;


public class PlayerUIScore : MonoBehaviour
{
    public static TextMeshProUGUI playerUIscore;

    public GameObject staplerPrefab;
    public Transform staplerShootParent;

    //Make sure to change the staplers back to 0 after testing
    public static int staplers = 100;
    public static Canvas mainCanvas;
    public GameObject rightHand;
    public static Audio audio;
    private static int techScore = 0;
    private static int custServScore = 0;
    private static int profScore = 0;
    private static int totalScore = 0;
    private GameObject staplerShoot;

    void Start()
    {
        playerUIscore = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        mainCanvas = gameObject.GetComponent<Canvas>();
        mainCanvas.enabled = false;
        audio = FindObjectOfType<Audio>();
        playerUIscore.SetText("Pro: " + profScore.ToString() + "\nTech: " + techScore.ToString() + "\nC-Srv: " + custServScore.ToString() + "\ntotal: " + totalScore.ToString() + "\nstaplers: " + staplers.ToString());
    }

    void Update()
    {     
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
            playerUIscore.SetText("Pro: " + profScore.ToString() + "\nTech: " + techScore.ToString() + "\nC-Srv: " + custServScore.ToString() + "\ntotal: " + totalScore.ToString() + "\nstaplers: " + staplers.ToString());
        }
    }


    public static void ScoreModify(int prof, int cs, int tech, bool correct, bool playSound)
    {
        profScore += prof;
        custServScore += cs;
        techScore += tech;
        if (playSound)
        {
            if (correct)
            {
                audio.correctSound();
            }
            else
            {
                audio.wrongSound();
            }
        }
        playerUIscore.SetText("Pro: " + profScore.ToString() + "\nTech: " + techScore.ToString() + "\nC-Srv: " + custServScore.ToString() + "\ntotal: " + totalScore.ToString() + "\nstaplers: " + staplers.ToString());
    }
}


