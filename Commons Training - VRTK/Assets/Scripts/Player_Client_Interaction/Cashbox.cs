using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Cashbox : MonoBehaviour
{
    private TextMeshPro displayText;
    private float textTimer = 0f;
    private string startingMessage = "To Add Funds, Please Insert Campus card";
    private bool startText = true;
    public Image lcdScreen;

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

        if (textTimer <= 0f && !startText)
            {
                displayText.fontSize = 108f;
                lcdScreen.color = new Color32(155, 192, 164, 255);
                displayText.text = startingMessage;
                startText = true;
            }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CampusCard"))
        {
            CashClient.cardChecked = true;
            displayText.fontSize = 150f;
            if (other.GetComponent<CampusCard>().expired)
                {
                    lcdScreen.color = new Color32(219, 85, 95, 255);
                    displayText.text = " \n Expired";
                }
            else
                {
                    displayText.text = " \n Balance: $100";
                }

            startText = false;
            textTimer = 5f;
        }
        
    }
}

