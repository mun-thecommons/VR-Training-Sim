using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public Animator doorAnim;

    public AudioSource doorAudio;
    public AudioClip doorOpen;
    public AudioClip doorClose;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            bool isOpen = !doorAnim.GetBool("IsOpen");

            // Toggle state of IsOpen Bool, trigger the button
            doorAnim.SetBool("IsOpen", isOpen);
            doorAnim.SetTrigger("ButtonPress");

            if (!isOpen)
            {
                doorAudio.PlayOneShot(doorClose);
            }
            else
            {
                doorAudio.PlayOneShot(doorOpen);
            }
        }

    }
}