using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class TestAnimatorController : MonoBehaviour
{
    Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown("space"))
        {
            animator.SetBool("Start Walk", true);
        }
        if (Input.GetKeyDown("tab"))
        {
            animator.SetBool("Turn Right", true);
        }
        /* if(Vector3.Distance(transform.position, ClientManager.destinationPositions[0].transform.position) <= 0)
        {
            animator.SetBool("Stop", true);
        } */
    }
}
