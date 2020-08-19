using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEngine.AI;
using OVRTouchSample;
using System.Text.RegularExpressions;
/// <summary>
/// Contains the logic used for the Cashbox Client, and how it interacts with the CampusCard and CashBox
/// 
/// ##Detailed
/// This script will interact with both the CampusCard and CashBox scripts so that the response of the Client is done correctly. 
/// The Renderer of the Campus Card attached to the NPC is initially set to inActive so that when the player interacts with them it has the effect
/// of being "Dropped" by the NPC. Once the Campus Card is returned it will go back to disabling the Renderer to make it appear as if
/// the NPC took their card back.
/// 
/// </summary>
public class CashClient : MonoBehaviour
{
    public GameObject greetings;
    private bool questionSetup = false;

    public TextMeshProUGUI cashCanvasText;
    public Image speechBalloon;
    public static bool cardChecked = false;
    
    private Client client;
    public GameObject player;
    [HideInInspector]
    public GameObject campusCard;
    public bool questionAnswered = false;

    private bool done = false;
    private float timer = 5f;

    void Start()
    {
        client = GetComponent<Client>();                            //Initial setup, finding proper objects
        campusCard = GameObject.Find("CampusCard");
        player = GameObject.FindGameObjectWithTag("Player");

    }
    
    /*******************************************************
     * Ensures proper setup of Interaction upon reaching destination position
     * 
     * ##Detailed
     * Upon reaching destination it will inform the Player that the NPC has a question/Interaction to complete. At this point the NPC will
     * ask the player to check their Campus Card for money and dependent on what the Blackbox says will determine the NPC's response. 
     * 
     * ****************************************************/
    void Update()
    {
        if (timer <= 0 && GetComponent<NavMeshAgent>().isStopped)
            {
                greetings.GetComponent<Canvas>().enabled = true;            //Once client reachs proper location, "!" appears above their head and they are ready to be interacted with
                questionSetup = true;
            }

        if (questionSetup && Vector3.Distance(transform.position, player.transform.position) < 3f)
        {

            if (OVRInput.GetDown(OVRInput.RawButton.A) && !cardChecked)
                {
                    timer = 5f;
                    Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                    transform.LookAt(targetPosition);
                    campusCard.GetComponent<MeshRenderer>().enabled = true;     // Campus Card will only appear once the player is within 3float distance, and presses the "A" Button
                    cashCanvasText.fontSize = 8;
                    speechBalloon.enabled = true;
                    cashCanvasText.text = "Excuse me, can you check if there is an issue with my campus card? Please try my card at the cash box.";
                }

            if (timer <= 0f && !Level.level2Cash)
                {
                    cashCanvasText.text = "";
                    speechBalloon.enabled = false;
                }

            if (cardChecked)        //Ensures card was swiped through Black box before client gives a response to player
                {
                    CardCheckResponse();
                }

        }

        if (cardChecked && questionSetup && Vector3.Distance(transform.position, player.transform.position) < 3f && GetComponent<NavMeshAgent>().isStopped)       // This allows for the client to look at the player when it is within 3 units of it
            {
                Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                transform.LookAt(targetPosition);
            }

        timer -= Time.deltaTime;
    }

    /*****************************
     * Informs the ClientManager that this instance has been destroyed
     * 
     * ##Detailed
     * The purpose of this is to allow the ClientManager to create new instances so that the Player can have the chance to interract with them again
     * 
     * **************************/
    private void OnDestroy()
    {
        ClientManager.cashClient = false;
    }

    /******************************************************
     * Determines the NPC's response to retrieval of CampusCard
     * 
     * ##Detailed
     * Based on the state that the CampusCard was set too will determine the NPC's response upon return. 
     * If this was the first time the player completed this task they are awarded points in Professionalism and Customer Service for doing so.
     * 
     * ****************************************************/
    void CardCheckResponse()
    {
        if (!Level.level2Cash)
            {
                timer = 3.5f;
                MasterController.ScoreModify(1, 1, 0, true, true);      // +1 Professionalism and +1 Customer Service
                Level.level2Cash = true;
                speechBalloon.enabled = true;
            }

        if (campusCard.GetComponent<CampusCard>().expired)
            {
                cashCanvasText.text = "Oh my! Thank you I never realized it was expired!";
            }

        else
            {
                cashCanvasText.text = "That seems like more than I remember, but perfect! Thank you!";
            }

        if (timer <= 0f)
            {
                cashCanvasText.text = "";
                speechBalloon.enabled = false;
                campusCard.GetComponent<MeshRenderer>().enabled = false;
                GetComponent<NavMeshAgent>().destination = new Vector3(-29.13f, 0.08f, 39.73f);
                GetComponent<TestAnimatorController>().animator.SetBool("CardReturned", true);
                GetComponent<NavMeshAgent>().isStopped = false;
            }
        Destroy(gameObject, 30f);
        Debug.Log(transform.position);

    }
}