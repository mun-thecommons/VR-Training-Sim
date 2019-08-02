using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaplerController : MonoBehaviour
{    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Hand"))
        {
            CollectibleManager.CollectStapler(transform.position);
            Destroy(gameObject);            
        }
    }
}
