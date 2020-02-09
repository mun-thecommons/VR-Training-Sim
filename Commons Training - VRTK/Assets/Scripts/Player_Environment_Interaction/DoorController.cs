using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    bool isOpen = false;

    private Animator doorAnim;
    private Collider[] colliders;

    private AudioSource doorAudio;
    public AudioClip doorOpen;

    void Start()
    {
        doorAnim = GetComponent<Animator>();
        colliders = GetComponents<Collider>();
        doorAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hand"))
        {
            // Toggle state of IsOpen Bool, trigger the button
            doorAnim.SetBool("IsOpen", !doorAnim.GetBool("IsOpen"));
            doorAnim.SetTrigger("ButtonPress");
            doorAudio.PlayOneShot(doorOpen);
        }
    
    }
}
