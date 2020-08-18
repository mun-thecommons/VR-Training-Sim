using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Placeholder script for CollectibleManager
/// 
/// ##Detailed
/// Informs the CollectibleManager script if a Scrap Paper object has been collected and then destroys the object.
/// This allows for a smaller less complex script for understanding
/// 
/// @See CollectibleManager; CoinController
/// 
/// @note At a later point both the CoinController and ScrapPaperController scripts may be replaced with one singular script, 
/// or the functions may be placed within the CollectibleManager script. 
/// These are ideas for future development
/// </summary>
public class ScrapPaperController : MonoBehaviour
{
    /*********************
     * This script will only interact with the Hand of the Player
     * 
     * After, it ensures that the collision is in fact with the Hand of the player (via tags) the script
     * will inform the CollectibleManager script, and then be destroyed
     * 
     * Collecting can only begin after the Redvest has been Collected (See MasterController for Red Vest)
     * ********************/
    void OnTriggerEnter(Collider collider)
    {
        if(MasterController.vestCollected && collider.CompareTag("Hand"))
        {
            CollectibleManager.CollectScrapPaper(transform.position);
            Destroy(gameObject);
        }
    }
}
