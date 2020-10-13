using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
/// <summary>
/// ##Script Description
/// This script is one of three that control the desk phones
/// The main functionality of the phone is stored here. The script connects the "cable" from the phone to 
/// the ethernet connector, and also handles the current state of the phone (plugged in or not)
/// </summary>
public class PhoneController : MonoBehaviour
{
    public bool pluggedIn = false; /*!< @brief The current state of this phone. The GameManager checks this variable to update the game state  */

    public GameObject ethernetConnector;
    public GameObject phone;
    public LineRenderer ethernetCable;

    public AudioClip plugInAudio;


    // Update is called once per frame
    void Update()
    {
        if (!pluggedIn && ethernetCable.gameObject != null) // Set the position of the phone cord to connect the phone to the ethernet plug
        {
            ethernetCable.SetPosition(0, phone.transform.localPosition);
            ethernetCable.SetPosition(1, ethernetConnector.transform.localPosition);
        }
       
        if (pluggedIn && ethernetConnector != null) // When the phone is plugged in, delete the cable and the plug (the plugged in ethernet cable is already a part of the port)
        {
            Destroy(ethernetConnector);
            Destroy(ethernetCable.gameObject);
            GameManager.playerAudioSource.PlayOneShot(plugInAudio, 1);
        }
    }
}
