using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Control the action of an alien being shot
/// The 'Dead' animation being displayed and the alien being destroyed when being shot
/// </summary>
public class AlientShot : MonoBehaviour
{
    private Animator alienAnimator;
    private NavMeshAgent alienNavMeshAgent;
   
    private void Start()
    {

        alienAnimator = gameObject.GetComponent<Animator>();
        alienNavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    /******************************
     * When an alien collides with a shot stapler, the alien will be destroyed
     * @note the "Dead" triggers "being shot" animation of the alien
     * ****************************/
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
