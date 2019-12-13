using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


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

    // Update is called once per frame
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
            //animator.SetBool("CardReturned", true);         //Used by CashClient... Once player has checked their balance and returned the card the client walks away
            navAgent.isStopped = false;
        }
    }
}