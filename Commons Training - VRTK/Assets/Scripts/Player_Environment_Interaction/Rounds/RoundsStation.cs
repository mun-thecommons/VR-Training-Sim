using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundsStation : MonoBehaviour
{

    public bool stationVisited = false;

    public void SetLightColour(Material colour)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("roundsCard") && !stationVisited)
        {
            RoundsController.stationsVisited++;
            stationVisited = true;
        }
    }
}
