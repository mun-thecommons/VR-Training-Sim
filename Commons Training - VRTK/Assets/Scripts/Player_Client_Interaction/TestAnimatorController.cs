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
        if (Level.level3ClientDesk)
        {
            animator.SetBool("Finished", true);
            GetComponent<NavMeshAgent>().isStopped = false;
        }
        if (Level.level2Cash)
        {
           // animator.SetBool("CardReturned", true);
            GetComponent<NavMeshAgent>().isStopped = false;
        }
    }
}