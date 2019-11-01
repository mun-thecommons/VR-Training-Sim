using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapPaperController : MonoBehaviour
{

    void OnTriggerEnter(Collider collider)
    {
        if(MasterController.vestCollected && collider.CompareTag("Hand"))
        {
            CollectibleManager.CollectScrapPaper(transform.position);
            Destroy(gameObject);
        }
    }
}
