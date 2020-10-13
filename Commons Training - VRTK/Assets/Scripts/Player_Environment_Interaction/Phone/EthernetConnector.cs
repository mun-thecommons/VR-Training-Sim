using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ##Script Description
/// This script is one of three that control the desk phones
/// It simply resets the connector back to its inital position if the player lets go of it
/// </summary>
public class EthernetConnector : MonoBehaviour
{

    // Store the initial location and rotation of the ethernet cable
    private Vector3 initialLocation;
    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        initialLocation = transform.position;
        initialRotation = transform.rotation;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand")) // Has the player moved their hand away from the connector? If so reset its position
        {
            transform.position = initialLocation;
            transform.rotation = initialRotation;
        }
    }
}
