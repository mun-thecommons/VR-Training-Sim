using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEngine.AI;
using OVRTouchSample;
using System.Text.RegularExpressions;

public class CashClient : MonoBehaviour
{
    public GameObject greetings;
    private bool questionSetup = false;

    public TextMeshProUGUI cashCanvasText;
    public static bool cardChecked = false;
    
    private Client client;
    public GameObject player;
    [HideInInspector]
    public GameObject campusCard;
    public bool questionAnswered = false;

    private bool done = false;
    private float timer = 0f;

    void Start()
    {
        client = GetComponent<Client>();                            //Initial setup, finding proper objects
        campusCard = GameObject.Find("CampusCard");
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        if (GetComponent<NavMeshAgent>().isStopped && !questionSetup)
            {
                greetings.GetComponent<Canvas>().enabled = true;            //Once client reachs proper location, "!" appears above their head and they are ready to be interacted with
                questionSetup = true;
            }

        if (questionSetup && Vector3.Distance(transform.position, player.transform.position) < 3f)
        {

            if (OVRInput.GetDown(OVRInput.RawButton.A))
                {
                    timer = 5f;
                    Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                    transform.LookAt(targetPosition);
                    campusCard.GetComponent<MeshRenderer>().enabled = true;     // Campus Card will only appear once the player is within 3float distance, and presses the "A" Button
                    cashCanvasText.fontSize = 9;
                    cashCanvasText.text = "Excuse me, can you check if there is an issue with my campus card? Please try my card at the cash box.";
                }

            if (timer <= 0f && !Level.level2Cash)
                {
                    cashCanvasText.text = "";
                }

            if (cardChecked)        //Ensures card was swiped through Black box before client gives a response to player
                {
                    CardCheckResponse();
                }

        }

        if (cardChecked && questionSetup && Vector3.Distance(transform.position, player.transform.position) < 3f)       // This allows for the client to lookat at the player when it is within 3 units of it
        {
            Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(targetPosition);
        }

        timer -= Time.deltaTime;
    }

    private void OnDestroy()
    {
        ClientManager.cashClient = false;
    }


    void CardCheckResponse()
    {
        if (!Level.level2Cash)
            {
                timer = 3f;
            }

        if (campusCard.GetComponent<CampusCard>().expired)
            {
                cashCanvasText.text = "Oh my! Thank you I never realized it was expired!";

            }

        else
            {
                cashCanvasText.text = "That seems like more than I remember, but perfect! Thank you!";
            }

        MasterController.ScoreModify(1, 1, 0, true, true);      // +1 Professionalism and +1 Customer Service
        Level.level2Cash = true;

        if (timer <= 0f)
            {
                cashCanvasText.text = "";
                GetComponent<NavMeshAgent>().destination = new Vector3(-29.13f, 0.08f, 39.73f);
                GetComponent<TestAnimatorController>().animator.SetBool("CardReturned", true);
            }

        Destroy(campusCard, 3f);
        Destroy(gameObject, 20f);
        Debug.Log(transform.position);
        Debug.Log(GetComponent<NavMeshAgent>().destination);

    }
}