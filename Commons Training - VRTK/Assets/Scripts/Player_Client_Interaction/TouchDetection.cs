using UnityEngine;

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

    public void ITSisGrabbed()
    {
        if (isTouchingITS && (OVRInput.Get(OVRInput.RawButton.LIndexTrigger) || OVRInput.Get(OVRInput.RawButton.RIndexTrigger)))
        {
            isGrabbingITS = true;
        }
        else
            isGrabbingITS = false;
    }

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