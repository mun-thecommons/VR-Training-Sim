using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundsController : MonoBehaviour
{

    // Static Variables for rounds timer
    // and how many stations have been visited
    public static float lastRounds;
    public static int stationsVisited;
    
    private GameObject[] roundsStations;
    private GameObject roundsCard;

    void Awake()
    {
        lastRounds = 0.0f;
        stationsVisited = 0;
        
        // Get all rounds stations and the rounds card
        roundsStations = GameObject.FindGameObjectsWithTag("Rounds Station");
        roundsCard = GameObject.FindWithTag("Rounds Card");
    }
    
    void Update()
    {
        lastRounds += Time.deltaTime;
        
        // Reset lastRounds counter after all 4 stations are visited
        if (stationsVisited >= 4)
        {
            stationsVisited = 0;
            lastRounds = 0.0f;
        }
    }
}
