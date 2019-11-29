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
{   public GameObject greetings;
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
        client = GetComponent<Client>();
        campusCard = GameObject.Find("CampusCard");
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        if (GetComponent<NavMeshAgent>().isStopped && !questionSetup)
        {
            greetings.GetComponent<Canvas>().enabled = true;
            questionSetup = true;
        }

        if (questionSetup && Vector3.Distance(transform.position, player.transform.position) < 3f)
        {

            if (OVRInput.GetDown(OVRInput.RawButton.A))
            {
                timer = 5f;
                campusCard.GetComponent<MeshRenderer>().enabled = true;
                cashCanvasText.fontSize = 9;
                cashCanvasText.text = "Excuse me, can you check if there is an issue with my campus card? Please try my card at the cash box.";
            }

            if(timer <= 0f && !Level.level2Cash)
            {
                cashCanvasText.text = "";
            }

            if(cardChecked)
            {
                CardCheckResponse();
            }

        }

        if (cardChecked && questionSetup && Vector3.Distance(transform.position, player.transform.position) < 3f)
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
        MasterController.ScoreModify(1, 1, 0, true, true);
        Level.level2Cash = true;
        if (timer <= 2f)
        {
            cashCanvasText.text = "";
            GetComponent<NavMeshAgent>().destination = new Vector3(-30.28f, 0.08f, -35.9f);
            GetComponent<TestAnimatorController>().animator.SetBool("CardReturned", true);
        }

        Destroy(campusCard, 2f);
        Destroy(gameObject, 10f);
        Debug.Log(transform.position);
        Debug.Log(GetComponent<NavMeshAgent>().destination);

    }
}