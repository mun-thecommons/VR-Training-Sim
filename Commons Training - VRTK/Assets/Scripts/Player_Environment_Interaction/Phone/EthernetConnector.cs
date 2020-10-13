using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            transform.position = initialLocation;
            transform.rotation = initialRotation;
        }
    }
}
