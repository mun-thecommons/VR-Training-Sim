using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
//import libraries

/// <summary>
/// ##Script Description
/// The script controls the action of completing rounds
/// The player can up level by completing rounds
/// When rounds are needed, round stations are enabled
/// </summary>
public class RoundsController : MonoBehaviour
{

    // Static Variables for rounds timer
    // and how many stations have been visited
    public static float lastRounds;
    public static float roundsInterval;
    public static int stationsVisited;

    
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
        /*
         * after all round stations are visitted, the counters are reset and there is no longer need to do rounds
         * @warning: the levels of the game needs to be reprogrammed
         */
        if (stationsVisited >= roundsStations.Length)
        {
            lastRounds = 0.0f;
            stationsVisited = 0;
            GameManager.roundsNeeded = false;
        }

        // Enable all rounds stations when rounds are needed again
        /*
         * All round stations are enabled when rounds are needed 
         * 
         */
        if (lastRounds >= roundsInterval && !GameManager.roundsNeeded)
        {
            foreach (GameObject station in roundsStations)
            {
                station.GetComponent<RoundsStation>().stationEnabled = true;
            }
            GameManager.roundsNeeded = true;
        }
    }
}
