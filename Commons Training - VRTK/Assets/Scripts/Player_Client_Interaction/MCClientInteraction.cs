using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Contains the Logic used for the MCClient interaction
/// 
/// ##Detailed
/// This script will either allow or disallow the player to interact with the MCClients around the library based on if they are wearing their RedVest.
/// 
/// </summary>
public class MCClientInteraction : MonoBehaviour
{
    public GameObject clientManager;
    public GameObject player;
    public Canvas questionsCanvas;

    public Canvas clientCanvas;

    private void Start()
    {
        //canvas = GetComponent<Canvas>();
    }
    /**************************
     * The Player must hit 'A' Button to interact with the Client
     * 
     * ##Detailed
     * If the Player is not currently wearing their RedVest the client will simply inform them to "Stop bothering me" and hitting 'A' does nothing.
     * 
     * ***********************/
    void Update()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = MasterController.vestCollected ? "Hit A to interact" : "Stop bothering me";
        checkClients();
    }

    /****************************
     * Checks which client the Player is interacting with (Phone, MC, Cash)
     * 
     * ##Detailed
     * This function wil check what questions are housed on the Parent GameObject. 
     * Afterward, it checks the distance between the player and the client, once they are 3float away the Canvas will appear telling the player 
     * too "Hit A to interact".
     * 
     * 
     * **************************/
    void checkClients()
    {
        foreach (Transform child in clientManager.transform)
        {
            if(!(child.GetComponent<MCQuestions>() != null || child.GetComponent<CashClient>() != null || child.GetComponent<PhoneBasedQuestions>() != null))
            {
                continue;
            }
            if(Vector3.Distance(child.position, player.transform.position) < 3f & !questionsCanvas.enabled)
            {
                clientCanvas.enabled = true;
                return;
            }
        }
        clientCanvas.enabled = false;
    }
}
