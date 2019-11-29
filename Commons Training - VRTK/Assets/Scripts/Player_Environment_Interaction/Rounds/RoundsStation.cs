using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundsStation : MonoBehaviour
{

    public bool stationVisited = false;
    public AudioClip swipeComplete;

    private AudioSource stationAudio;

    private void Start()
    {
        stationAudio = GetComponent<AudioSource>();
    }

    public void SetLightColour(Material colour)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RoundsCard") && !stationVisited)
        {
            RoundsController.stationsVisited++;
            stationVisited = true;

            stationAudio.PlayOneShot(swipeComplete);
        }
    }
}
