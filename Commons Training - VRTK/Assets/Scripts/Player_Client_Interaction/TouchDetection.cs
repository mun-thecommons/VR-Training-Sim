using UnityEngine;

/// <summary>
/// Logic used for the Phone based questions at the desk
/// 
/// ##Detailed
/// This script uses four Bool variables to keep track where the player is touching or what they are grabbing. Dependent upon the 
/// question that is asked of the player if they touch the correct phone (ITS or LabNet) they will have answered the question correctly.
/// 
/// If the player is grabbing a phone, this logic is used elsewhere so that if they player lets go after grabbing
/// the phone can reset its position to be back in its correct location
/// </summary>
public class TouchDetection : MonoBehaviour
{
   
    [HideInInspector]
    static public bool isTouchingITS = false;
    [HideInInspector]
    static public bool isTouchingLabNet = false;
    [HideInInspector]
    static  public bool isGrabbingITS = false;
    [HideInInspector]
    static public bool isGrabbingLabnet = false;

    void Update()
    {
        ITSisGrabbed();
        LabNetisGrabbed();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ITS"))
        {
            isTouchingITS = true;
        }
        else if (other.CompareTag("LabNet"))
        {
            isTouchingLabNet = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ITS"))
        {
            isTouchingITS = false;
        }


        else if (other.CompareTag("LabNet"))
        {
            isTouchingLabNet = false;
        }
       
    }
    /*******************************
     * Checks if ITS  Phone is being held by Player
     * 
     * @note This may be a redundant function as the script PhoneGrab does this as well
     * ****************************/
    public void ITSisGrabbed()
    {
        if (isTouchingITS && (OVRInput.Get(OVRInput.RawButton.LIndexTrigger) || OVRInput.Get(OVRInput.RawButton.RIndexTrigger)))
        {
            isGrabbingITS = true;
        }
        else
            isGrabbingITS = false;
    }
    /*******************************
     * Checks if LabNet  Phone is being held by Player
     * 
     *  @note This may be a redundant function as the script PhoneGrab does this as well
     * ****************************/
    public void LabNetisGrabbed()
    {
        if (isTouchingLabNet && (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) || OVRInput.Get(OVRInput.RawButton.LIndexTrigger)))
        {
            isGrabbingLabnet = true;           
        }
        else
            isGrabbingLabnet = false;
            

    }
}