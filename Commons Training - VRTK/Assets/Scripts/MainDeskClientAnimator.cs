﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MainDeskClientAnimator : MonoBehaviour
{
    Animator animator;
    public Vector3 goal;
    public Vector3 deskGoal;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<NavMeshAgent>().remainingDistance <= 1 && !animator.GetBool("Stop"))
        {
            animator.SetBool("Stop", true);
            GetComponent<NavMeshAgent>().isStopped = true;
        }
        if (PhoneBasedQuestions.animateWalk == true)
        {
            animator.SetBool("Finished", true);
            animator.SetBool("Stop", false);
            GetComponent<NavMeshAgent>().isStopped = false;
        }

    }
}
