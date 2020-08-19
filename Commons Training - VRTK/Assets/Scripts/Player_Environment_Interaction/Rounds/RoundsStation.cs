
using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
/// <summary>
/// ##Script Description
/// This script controls the behaviour of the round stations
/// When a round station is enabled, the round light will be blinking constantly
/// When the player swipes card at a round station, the swipe will be recorded, and there will be indication with light and sound
/// </summary>
public class RoundsStation : MonoBehaviour
{
   //is the round station enabled
    public bool stationEnabled = true; 
    //audio played when swipe complete
    public AudioClip swipeComplete; 

    public GameObject roundsLight;

    public Light roundsPointLight;


    private Renderer roundsLightRenderer;

    private AudioSource stationAudio;

    // Timer for red light blinking and how quickly it should blink
    private float blinkTimer;
    private float blinkSpeed;

    private void Start()
    {
        stationAudio = GetComponent<AudioSource>();
        roundsLightRenderer = roundsLight.GetComponent<Renderer>();


        blinkTimer = 0.0f;
        blinkSpeed = 1.0f;
    }



    private void Update()
    {
        if (stationEnabled)
        {
            blinkTimer += Time.deltaTime;

            if (blinkTimer >= blinkSpeed)
            {
                if (roundsLightRenderer.material.color == Color.red)
                {
                    roundsLightRenderer.material.SetColor("_Color", Color.black);
                    roundsPointLight.color = Color.black;
                }
                else
                {
                    roundsLightRenderer.material.SetColor("_Color", Color.red);
                    roundsPointLight.color = Color.red;
                }

                blinkTimer = 0.0f;
            }
        }
        else
        {
            roundsLightRenderer.material.SetColor("_Color", Color.green);
            roundsPointLight.color = Color.green;
        }
    }
    /*
     * When the player swipes card to an enabled round station, the swipe will be registered and the round station will be disabled 
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RoundsCard") && stationEnabled)
        {
            RoundsController.stationsVisited++;
            stationEnabled = false;

            stationAudio.PlayOneShot(swipeComplete);
        }
    }
}
