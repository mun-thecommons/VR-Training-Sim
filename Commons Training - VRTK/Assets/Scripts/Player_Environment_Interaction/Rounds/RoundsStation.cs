using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundsStation : MonoBehaviour
{

    public bool stationEnabled = true;
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
