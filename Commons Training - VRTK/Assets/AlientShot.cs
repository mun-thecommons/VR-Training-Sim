using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlientShot : MonoBehaviour
{
    private Animator alienAnimator;

    private void Start()
    {
        alienAnimator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StaplerThrow"))
        {
            alienAnimator.SetBool("Dead", true);
        }
    }
}
