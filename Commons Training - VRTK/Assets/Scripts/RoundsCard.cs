using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundsCard : MonoBehaviour {

    private float currentPillarDistance = Mathf.Infinity;
    private float newPillarDistance;
    private GameObject[] roundPillars;
    private GameObject closestPillar;
    private AudioSource singleRoundSignal;
    // Use this for initialization
    
    private void Start()
    {
        singleRoundSignal = gameObject.GetComponent<AudioSource>();
        roundPillars = GameObject.FindGameObjectsWithTag("RoundPillar");
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

            foreach(GameObject g in roundPillars)
            {
                newPillarDistance = Vector3.Distance(other.transform.position, g.transform.position);
                if (newPillarDistance < currentPillarDistance)
                {
                    currentPillarDistance = newPillarDistance;
                    closestPillar = g;
                }
            }

            closestPillar.GetComponent<Renderer>().material.color = Color.green;
            currentPillarDistance = Mathf.Infinity;
        }        
    }

    

}
