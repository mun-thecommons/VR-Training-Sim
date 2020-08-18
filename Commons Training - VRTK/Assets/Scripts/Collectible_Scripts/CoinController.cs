using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Placeholder script for CollectibleManager
/// 
/// ##Detailed
/// Informs the CollectibleManager script if a coin has been collected and then destroys the coin.
/// This allows the coins to only need this smaller less complex script attached for understanding
/// 
/// @See CollectibleManager; ScrapPaperController (virtually same script)
/// 
/// @note At a later point both the CoinController and ScrapPaperController scripts may be replaced with one singular script, 
/// or the functions may be placed within the CollectibleManager script. 
/// These are ideas for future development
/// </summary>
public class CoinController : MonoBehaviour
{
    /*********************
     * This script will only interact with the Hand of the Player
     * 
     * After, it ensures that the collision is in fact with the Hand of the player (via tags) the script
     * will inform the CollectibleManager script, and then be destroyed
     * 
     * Collecting can only begin after the Redvest has been Collected (See MasterController for Red Vest)
     * ********************/
    private void OnTriggerEnter(Collider other)
    {
        if (MasterController.vestCollected && other.CompareTag("Hand"))
        {
            CollectibleManager.CollectCoin(transform.position);
            Destroy(gameObject);
        }
    }
}
