using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ##Script Description
/// This script is one of three that control the desk phones
/// It checks to see if the player successfully plugged the plug into the port.
/// When this happens, the script sets the phone's pluggedIn bool to true. It
/// also makes the two status lights on the port turn green for visual feedback
/// </summary>
public class PortController : MonoBehaviour
{
    public PhoneController attachedPhone;
    public GameObject portConnector;

    public GameObject[] statusLights = new GameObject[2]; /*!< @brief Array to hold the two cubes representing the status lights  */
    public Light statusPointLight; /*!< @brief The light GameObject  */

    // Timer for red light blinking and how quickly it should blink
    private float blinkTimer;
    private float blinkSpeed;

    // Start is called before the first frame update
    void Start()
    {
        portConnector.SetActive(false); // Hide the plugged in connector until the player actually plugs it in

        blinkTimer = 0.0f;
        blinkSpeed = 0.8f;
    }


    void Update()
    {
        if (!attachedPhone.pluggedIn)
        {
            blinkTimer += Time.deltaTime;

            if (blinkTimer >= blinkSpeed)
            {
                if (statusLights[0].GetComponent<Renderer>().material.color == Color.red)
                {
                    foreach (GameObject light in statusLights)
                    {
                        light.GetComponent<Renderer>().material.SetColor("_Color", Color.black); // Make both the lights turn green
                    }
                    statusPointLight.color = Color.black; // Turn the actual illumination green
                }
                else
                {
                    foreach (GameObject light in statusLights)
                    {
                        light.GetComponent<Renderer>().material.SetColor("_Color", Color.red); // Make both the lights turn green
                    }
                    statusPointLight.color = Color.red; // Turn the actual illumination green
                }

                blinkTimer = 0.0f;
            }
        }
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EthernetConnector")) // Did the player "plug in" the connector (i.e. bring the plug close to the port)
        {
            foreach (GameObject light in statusLights)
            {
                light.GetComponent<Renderer>().material.SetColor("_Color", Color.green); // Make both the lights turn green
            }
            statusPointLight.color = Color.green; // Turn the actual illumination green
            attachedPhone.pluggedIn = true; // Set the phone's state
            portConnector.SetActive(true); // Turn on the port's connector so the player sees that it's plugged in
        }
    }
}
