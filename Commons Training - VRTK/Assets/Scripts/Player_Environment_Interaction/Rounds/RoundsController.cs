using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundsController : MonoBehaviour
{

    // Static Variables for rounds timer
    // and how many stations have been visited
    public static float lastRounds;
    public static float roundsInterval;
    public static int stationsVisited;

    public bool roundsNeeded = false;
    
    private GameObject[] roundsStations;


    void Awake()
    {
        lastRounds = 1800.0f;

        // Rounds must be completed every 30 minutes
        roundsInterval = 1800.0f;
        stationsVisited = 0;
        
        // Get all rounds stations and the rounds card
        roundsStations = GameObject.FindGameObjectsWithTag("RoundsStation");
    }
    
    void Update()
    {
        lastRounds += Time.deltaTime;
        
        // Reset lastRounds counter after all stations are visited
        if (stationsVisited >= roundsStations.Length)
        {
            lastRounds = 0.0f;
            stationsVisited = 0;
            roundsNeeded = false;

            if (Level.level == 2)
            {
                Level.level2Round = true;
            }
            MasterController.ScoreModify(1, 0, 0, true, false);
        }

        // Enable all rounds stations when rounds are needed again
        if (lastRounds >= roundsInterval && !roundsNeeded)
        {
            foreach (GameObject station in roundsStations)
            {
                station.GetComponent<RoundsStation>().stationEnabled = true;
            }
            roundsNeeded = true;
        }
    }
}
