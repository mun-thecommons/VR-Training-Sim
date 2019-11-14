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
    private bool appear = false;
    public GameObject greetings;
    private bool questionSetup = false;
    private Canvas mcClientInteraction;

    public TextMeshProUGUI cashCanvasText;

    private Client client;
    public GameObject player;
    private MeshRenderer campusCard;

    private bool questionAsked = false;
    public bool questionAnswered = false;

    private bool done = false;
    private float timer = 0f;

    void Start()
    {
        mcClientInteraction = GameObject.FindGameObjectWithTag("MCClientInteraction").GetComponent<Canvas>();
        client = GetComponent<Client>();
        campusCard = GameObject.FindGameObjectWithTag("CampusCard").GetComponent<MeshRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        if (client.askingQuestion && !questionAsked && timer <= 0 && !done)
        {
            questionAsked = true;
        }

        if (GetComponent<NavMeshAgent>().isStopped && !questionSetup)
        {
            greetings.GetComponent<Canvas>().enabled = true;
            questionSetup = true;
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 5f)
        {
            mcClientInteraction.enabled = (questionAnswered) ? false : true;

            if (mcClientInteraction.enabled == true && !appear)
            {
                Debug.Log("Should be Appearing");
                appear = true;
            }

            if (OVRInput.GetDown(OVRInput.RawButton.A))
            {
                
                campusCard.enabled = true;
                Debug.Log(campusCard.enabled);
                cashCanvasText.fontSize = 9;
                cashCanvasText.text = "Excuse me, can you check if there is an issue with my campus card? Please try my card at the cash box.";
                
            }
        }

        if (questionSetup && Vector3.Distance(transform.position, player.transform.position) < 5f)
        {
            Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(targetPosition);
        }

        timer -= Time.deltaTime;
    }
   
    
}