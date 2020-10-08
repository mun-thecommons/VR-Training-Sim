using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Open and close doors with the push of a button
/// 
/// ##Script Description
/// Control sci-fi doors with audio and animations. clicking the door's button will cause it to open and close
/// </summary>
public class DoorController : MonoBehaviour
{
    public Animator doorAnim;
    public AudioSource doorAudio;
    public AudioClip doorOpen;
    public AudioClip doorClose;

    public bool isEnabled = true;

    void Start()
    {
    }

    /*****************************************************************************************************
     * Detect the player pressing the button
     * 
     * The only collider that's part of the "Sci-fi door" object is the push button, the door colliders are
     * a part of the door sub-objects
     * 
    ***********************************************************/
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hand"))
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if (!isEnabled) { return; }
        bool isOpen = !doorAnim.GetBool("IsOpen"); // Toggle the current door state

        // Toggle state of IsOpen Bool, trigger the button
        doorAnim.SetBool("IsOpen", isOpen);
        doorAnim.SetTrigger("ButtonPress");

        if (!isOpen) // Play the right sound depending on if the door is opening or closing
        {
            doorAudio.PlayOneShot(doorClose);
        }
        else
        {
            doorAudio.PlayOneShot(doorOpen);
        }
    }
}

