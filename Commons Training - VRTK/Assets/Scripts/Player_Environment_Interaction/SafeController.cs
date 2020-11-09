using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeController : MonoBehaviour
{

    private Animator safeAnim;
    public AudioClip successAudio;
    // Start is called before the first frame update
    void Start()
    {
        safeAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand") && !GameManager.safeOpen)
        {
            GameManager.safeOpen = true;
            GameManager.playerAudioSource.PlayOneShot(successAudio);
            safeAnim.SetTrigger("Open");
        }
    }
}
