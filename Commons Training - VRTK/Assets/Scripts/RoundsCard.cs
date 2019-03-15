using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundsCard : MonoBehaviour {
    public static bool isRound1Done = false;
    public static bool isRound2Done = false;
    public static bool isRound3Done = false;
    public static bool isRound4Done = false;
    private AudioSource singleRoundSignal;
    // Use this for initialization


    private void Start()
    {
        singleRoundSignal = gameObject.GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Station1Light")
        {
           isRound1Done = true;
            singleRoundSignal.Play();
        }
        if (collision.gameObject.name == "Station2Light")
        {
           isRound2Done = true;
            singleRoundSignal.Play();
        }
        if (collision.gameObject.name == "Station3Light")
        {
           isRound3Done = true;
            singleRoundSignal.Play();
        }
        if (collision.gameObject.name == "Station4Light")
        {
           isRound4Done = true;
            singleRoundSignal.Play();
        }
    }
}
