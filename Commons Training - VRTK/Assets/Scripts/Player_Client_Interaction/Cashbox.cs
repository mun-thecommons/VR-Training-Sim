using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
/// <summary>
/// Houses the Logic of the Cashbox which is used in tandem with the Campus Card
/// 
/// ##Detailed
/// Controls the LCD screen of the Cashbox. When the CampusCard is inserted into the slot dependent on which state the Campus card
/// is set too will determine what message will popo up on the LCD Screen. 
/// 
/// @see CampusCard
/// </summary>
public class Cashbox : MonoBehaviour
{
    private TextMeshPro displayText;            /*!< @brief Controls the Text displayed on the LCD Screen*/
    private float textTimer = 0f;
    private string startingMessage = "To Add Funds, Please Insert Campus card";     
    private bool startText = true;
    public Image lcdScreen;             /*!< @brief stores the digital grid image which makes the Cashbox more realistic*/

    // Use this for initialization
    void Start()
    {
        displayText = GetComponentInChildren<TextMeshPro>();
        displayText.text = startingMessage;
    }

    /*********************************
     * After a certain amount of time LCD screen will reset to default
     * 
     * ##Detailed
     * The timer will indicate how long a message has been displayed on the LCD screen. Once it has been displayed for so long it will reset 
     * the screen to its default colour as well as reset the text to the starting message.
     * 
     * *******************************/
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
    /*********************************
     * When in contact with the CampusCard, the logic will follow, otherwise nothing happens
     * 
     * ##Detailed
     * Once the Campus Card has come in contact with the Cashbox's card slot then the script will check the state of the card. Once
     * The state has been checked the LCD screen's text will changed appropriately, as well as the colour if the card is expired.
     * After 5 seconds have past the LCD screen will reset back to its default string message and colour.
     * 
     * *****************************/
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

