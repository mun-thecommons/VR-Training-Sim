using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cashbox : MonoBehaviour
{
    private TextMeshPro displayText;
    private float textTimer = 0f;
    private string startingMessage = "Please insert your card";
    private bool startText = true;

    // Use this for initialization
    void Start()
    {
        displayText = GetComponentInChildren<TextMeshPro>();
        displayText.text = startingMessage;
    }

    // Update is called once per frame
    void Update()
    {
        textTimer -= Time.deltaTime;
        if(textTimer <= 0f && !startText)
        {
            displayText.text = startingMessage;
            startText = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CampusCard"))
        {
            CashClient.cardChecked = true;
            if (other.GetComponent<CampusCard>().expired)
            {
                displayText.text = "Expired";
            }
            else
            {
                displayText.text = "Balance: $500";
            }
            startText = false;
            textTimer = 5f;
        }
        
    }
}

