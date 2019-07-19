using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class TestAnimatorController : MonoBehaviour
{
    Animator animator;
    public Vector3 goal;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, GetComponent<NavMeshAgent>().destination) <= 1 && !animator.GetBool("Stop"))
        {
            Debug.Log("here");
            //agent.autoRepath;
            animator.SetBool("Stop", true);                                             // Supposed to toggle the Stop variable and make the soldiers idle
            GetComponent<NavMeshAgent>().enabled = false;
        }
    }
}
