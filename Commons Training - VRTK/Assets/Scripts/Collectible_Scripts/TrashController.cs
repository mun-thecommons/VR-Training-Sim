using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script keeps track of how much trash is collected of each category
/// 
/// ##Detailed
/// This script will only run if the collision with the trash includes the "Hand" tag. Once a collision
/// has been detected then it will check the tag attached to the trash object and increment the following collection variable within the MasterController.
/// 
/// @see CollectibleManager; MasterController
/// </summary>
public class TrashController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            CollectibleManager.CollectTrash(gameObject.transform.position);

            if (gameObject.CompareTag("BaseTrash"))
            {
                MasterController.baseTrash++;
            }
            else if (gameObject.CompareTag("MetalTrash"))
            {
                MasterController.metalTrash++;
            }
            else if (gameObject.CompareTag("PlasticTrash"))
            {
                MasterController.plasticTrash++;
            }

            Destroy(gameObject);
        }
    }
}
