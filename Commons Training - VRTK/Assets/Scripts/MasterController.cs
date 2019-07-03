using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;


public class MasterController : MonoBehaviour
{
    public GameObject staplerPrefab;
    public Transform staplerParent;
    public GameObject rightHand;

    public static TextMeshProUGUI playerUIscore;
    public static TextMeshProUGUI scoreBreakDownText;
    public static TextMeshProUGUI totalScoreText;
    public static TextMeshProUGUI staplerCountText;
    public static TextMeshProUGUI mainFrameText;
    public static bool vestCollected = false;
    public static int staplers = 100;
    public static Canvas mainCanvas;    
    public static Audio audio;

    private static int techScore = 0;
    private static int custServScore = 0;
    private static int profScore = 0;
    private static int totalScore = 0;
    private GameObject staplerShoot;

    void Start()
    {
        //playerUIscore = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        scoreBreakDownText = GameObject.Find("ScoreDetailedBox").GetComponentInChildren<TextMeshProUGUI>();
        totalScoreText = GameObject.Find("XPointsBox").GetComponentInChildren<TextMeshProUGUI>();
        staplerCountText = GameObject.Find("StaplerBox").GetComponentInChildren<TextMeshProUGUI>();
        mainCanvas = gameObject.GetComponent<Canvas>();
        mainCanvas.enabled = false;
        audio = FindObjectOfType<Audio>();
        //playerUIscore.SetText("Pro: " + profScore.ToString() + "\nTech: " + techScore.ToString() + "\nC-Srv: " + custServScore.ToString() + "\ntotal: " + totalScore.ToString() + "\nstaplers: " + staplers.ToString());


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
            staplerShoot.transform.parent = staplerParent;
            staplers--;
            // playerUIscore.SetText("Pro: " + profScore.ToString() + "\nTech: " + techScore.ToString() + "\nC-Srv: " + custServScore.ToString() + "\ntotal: " + totalScore.ToString() + "\nstaplers: " + staplers.ToString());
            staplerCountText.SetText(staplers.ToString());
        }
    }

    public static void ScoreModify(int prof, int cs, int tech, bool correct, bool playSound)
    {
        profScore += prof;
        custServScore += cs;
        techScore += tech;
        totalScore = profScore + custServScore + techScore;
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
        scoreBreakDownText.SetText("Pro: " + profScore.ToString() + "\nTech: " + techScore.ToString() + "\nC-Srv: " + custServScore.ToString());
        totalScoreText.SetText(totalScore.ToString());
        //playerUIscore.SetText("Pro: " + profScore.ToString() + "\nTech: " + techScore.ToString() + "\nC-Srv: " + custServScore.ToString() + "\ntotal: " + totalScore.ToString() + "\nstaplers: " + staplers.ToString());
    }
}


