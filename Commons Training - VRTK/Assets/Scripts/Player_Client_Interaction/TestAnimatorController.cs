using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Logic used for the animations attached to the UMA Models
/// 
/// ##Detailed 
/// This script interacts with the animations of the UMA models. It interacts with the regular MC Clients and the PhoneClient.
/// 
/// </summary>
public class TestAnimatorController : MonoBehaviour
{
    public Animator animator;
    public Vector3 goal;
    public Vector3 deskGoal;
    public Vector3 cashGoal;
    private NavMeshAgent navAgent;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    /************************
     * Changes Current animations based on if conditions are met
     * 
     * For a better look at what this is doing, look at the animator agents being used by the UMA Model in question.
     * ********************/
    void Update()
    {
        if (navAgent.remainingDistance <= 1 && !animator.GetBool("Stop") && !navAgent.pathPending)
        {
            animator.SetBool("Stop", true);     //Generic for each model.. Allows them to stop and switch to a idle animation once they reach their goal
            navAgent.isStopped = true;
        }
        if (Level.level3ClientDesk)
        {
            animator.SetBool("Finished", true);             //Used by PhoneClient... Once player has answered their question the client walks away
            navAgent.isStopped = false;
        }
        if (Level.level2Cash)
        {
            //navAgent.isStopped = false;
        }
    }
}