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
    
    private GameObject[] roundsStations;
    private GameObject roundsCard;

    void Awake()
    {
        lastRounds = 0.0f;

        // Rounds must be completed every 30 minutes
        roundsInterval = 10.0f;
        stationsVisited = 0;
        
        // Get all rounds stations and the rounds card
        roundsStations = GameObject.FindGameObjectsWithTag("RoundsStation");
        roundsCard = GameObject.FindWithTag("RoundsCard");
    }
    
    void Update()
    {
        Debug.Log(stationsVisited);

        lastRounds += Time.deltaTime;
        
        // Reset lastRounds counter after all stations are visited
        if (stationsVisited >= roundsStations.Length)
        {
            lastRounds = 0.0f;
            stationsVisited = 0;
        }

        // Enable all rounds stations when rounds are needed again
        if (lastRounds >= roundsInterval)
        {
            foreach (GameObject station in roundsStations)
            {
                station.GetComponent<RoundsStation>().stationEnabled = true;
            }
        }
    }
}
