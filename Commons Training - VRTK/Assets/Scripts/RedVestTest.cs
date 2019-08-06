using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

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
            grabble.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            audio.touchVestSound();
            MasterController.vestCollected = true;
            foreach(OVRGrabbable grabble in components)
            {
                grabble.enabled = true;
            }
            Destroy(gameObject);
        }
    }
}
