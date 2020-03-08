using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{


    public Animator doorAnim;
    private Collider[] colliders;

    public AudioSource doorAudio;
    public AudioClip doorOpen;
    public AudioClip doorClose;

    void Start()
    {
        colliders = GetComponents<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hand"))
        {
            Debug.Log("press!");

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

