using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlientShot : MonoBehaviour
{
    private Animator alienAnimator;
    private NavMeshAgent alienNavMeshAgent;
   
    private void Start()
    {

        alienAnimator = gameObject.GetComponent<Animator>();
        alienNavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StaplerThrow"))
        {
  
            alienAnimator.SetBool("Dead", true);
            alienNavMeshAgent.speed = 2;
            Destroy(gameObject,1.5f);
        }
    }

}
