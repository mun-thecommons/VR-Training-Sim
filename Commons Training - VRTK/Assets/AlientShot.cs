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
            alienNavMeshAgent.speed = 0;
            alienAnimator.SetBool("Dead", true);
            Destroy(gameObject,3);
            AlienManager.howManyAlien--;
        }
    }


}
