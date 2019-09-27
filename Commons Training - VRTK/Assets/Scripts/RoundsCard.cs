using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundsCard : MonoBehaviour {

    private Color originalPillarColor;
    public GameObject[] roundPillars;
    private AudioSource singleRoundSignal;
    // Use this for initialization
    
    private void Start()
    {
        singleRoundSignal = gameObject.GetComponent<AudioSource>();
        originalPillarColor = roundPillars[0].GetComponent<Renderer>().material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Station"))
        {
            RoundsLight station = other.gameObject.GetComponent<RoundsLight>();
            if (!station.isDone)
            {
                singleRoundSignal.Play();
                station.gameObject.GetComponent<Renderer>().material.color = Color.green;
                station.isDone = true;
                Rounds.checkRounds();
            }

            StartCoroutine(ChangePillarColor());
        }        
    }

    IEnumerator ChangePillarColor()
    {
        foreach(GameObject g in roundPillars)
        {
            g.GetComponent<Renderer>().material.color = Color.green;
        }

        yield return new WaitForSeconds(3);

        foreach(GameObject g in roundPillars)
        {
            g.GetComponent<Renderer>().material.color = originalPillarColor;
        }
    }

}
