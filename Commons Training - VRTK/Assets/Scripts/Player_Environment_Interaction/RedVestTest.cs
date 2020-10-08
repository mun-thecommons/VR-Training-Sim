using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

/// <summary>
/// ##Script Description
/// At the start of the game, all grabbable game objects cannot be interacted with
/// When the player collects the Red Vest, the grabbable game objects can be interacted with
/// </summary>
public class RedVestTest : MonoBehaviour
{
    OVRGrabbable[] components;
    public static Audio audio;

    void Start()
    {
        audio = GameObject.Find("LocalAvatar").GetComponent<Audio>();
        components = FindObjectsOfType<OVRGrabbable>();
        foreach (OVRGrabbable grabble in components)
        {
            //grabble.enabled = false;
        }
    }
    /*
     * When the Red Vest is collected, all grabbable objects are enabled 
     * @note: before the player collects the Red Vest, he/she cannot interact with grabbable game objects
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            audio.touchVestSound();
            MasterController.vestCollected = true;
            if(Level.level == 1)
            {
                Level.level1Vest = true;
            }
            foreach(OVRGrabbable grabble in components)
            {
                grabble.enabled = true;
            }
            Destroy(gameObject);
        }
    }
}
