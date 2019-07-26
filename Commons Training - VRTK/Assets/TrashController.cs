using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            TrashManager.CollectTrash(gameObject.transform.position);

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
