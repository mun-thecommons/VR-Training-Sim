using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PhoneGrab : MonoBehaviour {
    public Vector3 phoneOriginalPosition;
    public Quaternion phoneOriginalRotation;
    public bool isGrabbed;

    void Start()
    {
        isGrabbed = false;
        phoneOriginalPosition = transform.position;
        phoneOriginalRotation = transform.rotation;
        
    }

    
   
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            // GameObject.Find("PhoneBasedMale(Clone)").GetComponent<PhoneBasedQuestions>().questionAnswered = true;
           
            isGrabbed = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            isGrabbed = false;
            
            transform.position = phoneOriginalPosition;
            transform.rotation = phoneOriginalRotation;

        }
    }

}
