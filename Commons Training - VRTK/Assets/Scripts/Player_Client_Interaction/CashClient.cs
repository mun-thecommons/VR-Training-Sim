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

    
    private Client client;
    public GameObject player;
    [HideInInspector]
    public static MeshRenderer campusCard;

    private bool questionAsked = false;
    public bool questionAnswered = false;

    private bool done = false;
    private float timer = 0f;

    void Start()
    {
        client = GetComponent<Client>();
        campusCard = transform.Find("CampusCard").GetComponent<MeshRenderer>();
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
                
                campusCard.enabled = true;
                cashCanvasText.fontSize = 9;
                cashCanvasText.text = "Excuse me, can you check if there is an issue with my campus card? Please try my card at the cash box.";
                
            }
        }

        if (questionSetup && Vector3.Distance(transform.position, player.transform.position) < 3f)
        {
            Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(targetPosition);
        }

        timer -= Time.deltaTime;
    }
   
    
}