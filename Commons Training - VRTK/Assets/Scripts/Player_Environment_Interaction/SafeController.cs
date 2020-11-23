using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeController : MonoBehaviour
{

    private Animator safeAnim;

    // Start is called before the first frame update
    void Start()
    {
        safeAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand") && !GameManager.safeOpen)
        {
            if (GameManager.hasVest)
            {
                GameManager.safeOpen = true;
                GameManager.playerAudioSource.PlayOneShot(GameManager.successAudioClip);
                safeAnim.SetTrigger("Open");
            }
            else
            {
                GameManager.playerAudioSource.PlayOneShot(GameManager.deniedAudioClip);
            }

        }
    }
}
