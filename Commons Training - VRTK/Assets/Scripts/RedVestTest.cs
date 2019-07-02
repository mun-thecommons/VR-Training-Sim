using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedVestTest : MonoBehaviour
{
    OVRGrabbable[] components;

    void Start()
    {
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
            MasterController.vestCollected = true;
            foreach(OVRGrabbable grabble in components)
            {
                grabble.enabled = true;
            }
            Destroy(gameObject);
        }
    }
}
