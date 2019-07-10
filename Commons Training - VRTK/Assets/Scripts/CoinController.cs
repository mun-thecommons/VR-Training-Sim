using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            CoinManager.CollectCoin(transform.position);
            Destroy(gameObject);
        }
    }
}
