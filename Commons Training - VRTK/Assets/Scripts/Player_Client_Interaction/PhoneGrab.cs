using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script sets the default position of the Phone
/// 
/// ##Detailed
/// If the Phone is being held off the base but it is in the Players hand then nothing happens. But if the player lets go of the phone while
/// it is off its base then it will reset the phone to its original position.
/// </summary>
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

    
   /*************************
    * Used to detect if the Phone is being held currently
    * 
    * ***********************/
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            // GameObject.Find("PhoneBasedMale(Clone)").GetComponent<PhoneBasedQuestions>().questionAnswered = true;
           
            isGrabbed = true;
        }
    }
    /************************
     * Detects if the player let go of the Phone and sets it back to default position
     * 
     * **********************/
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
