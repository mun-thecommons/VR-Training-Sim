using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;
/// <summary>
/// Script houses the logic used for the PhonebasedClient. **(More then just questions)**
/// 
/// ##Detailed
/// This script will interact with the QuestionInput script to find the questions that are to be asked by the PhoneBasedClient, and then dependet upon which
/// question is chosen, the script will choose the correct audio snippet to execute. 
/// As well, this script holds the logic used for the walking animation to leave the desk.
/// 
/// </summary>
public class PhoneBasedQuestions : MonoBehaviour {

    public GameObject player;
    [HideInInspector]
    public static float timer = 0f;
    public static bool done = false;
    public static bool animateWalk;     /*!< @brief Used for the Animator Controller */
    public GameObject questions;        /*!< @brief Grabs the questions used from the QuestionInput script */
    [HideInInspector]
    public GameObject answers;          /*!< @brief Grabs the answers used from the QuestionInput script */
    private Client client;
    private bool questionAsked = false;
    public float questionDelay = 5f;
    private int randomIndex;
    private string answer;
    public PhoneGrab ITS;
    public PhoneGrab Labnet;
    private Audio audio;

    // Use this for initialization
    void Start ()
    {
        animateWalk = false;
        player = GameObject.FindGameObjectWithTag("Player");
        questions = gameObject.transform.Find("QuestionCanvas").gameObject;
        Labnet = GameObject.Find("Phone Labnet").GetComponentInChildren<PhoneGrab>();
        ITS = GameObject.Find("Phone ITS").GetComponentInChildren<PhoneGrab>();
        client = GetComponent<Client>();
        questions.SetActive(false);
        audio = FindObjectOfType<Audio>();
    }

    /**************************************************
     * Script executes commands based upon how much distance is between the Client and player
     * 
     * ##Detailed
     * Once the Client comes within a certain distance of the Player the script will run its logic.
     * The script will select a random number between 0 and the amount of questions within the questionsArray of the QuestionInput. 
     * Dependent on what question is chosen the script will look to see if it matches with a specific audio clip, once it verifies that it doesz it will play the correct audio clip.
     * 
     * If the player is not wearing their redvest the NPC will instead ask for an employee (Signalling the player is doing something incorrectly)
     * 
     * Similar process that occurs for the questions will occur for the answers, so that when the player chooses a phone (LabNet or ITS) the script knows whether the correct answer was chosen.
     * 
     * ************************************************/
    void Update()
    {
        if(Vector3.Distance(player.transform.position , transform.position) <= 3)
        {
            questions.SetActive(true);
            client.askingQuestion = true;
        }
        else
        {
            questions.SetActive(false);
            client.askingQuestion = false;
        }
        //CLIENT 1 - THE NON-MC
        if (done && timer <= 0f)
        {
            done = false;
            animateWalk = true;
            Debug.Log("??");
            GetComponent<NavMeshAgent>().destination = new Vector3(-30.28f, 0.08f, -35.9f);
            Level.level3ClientDesk = true;
            Destroy(gameObject, 10f);
        }
        if (client.askingQuestion && !questionAsked && timer <= -5f)
        {
            if (MasterController.vestCollected)
            {
                randomIndex = Random.Range(0, QuestionInput.questionsArray.Count);
                questions.GetComponentInChildren<TextMeshProUGUI>().text = QuestionInput.questionsArray[randomIndex];
                answer = QuestionInput.answersArray[randomIndex];
                if (QuestionInput.questionsArray[randomIndex] == "My files aren't showing up on my desktop when I login!")
                    audio.noFilesSound();
                if (QuestionInput.questionsArray[randomIndex] == "My balance is negative but I've never printed anything before?")
                    audio.balanceNegativeSound();
                if (QuestionInput.questionsArray[randomIndex] == "I cannot login into your computers or my.mun.ca. I tried reseting my password but that does not work.")
                    audio.noLoginSound();
                if (QuestionInput.questionsArray[randomIndex] == "None of my email is showing up in my inbox for MUNmail. I used to be staff.")
                    audio.noEmailSound();
                questionAsked = true;
            }
            else
            {
                questions.GetComponentInChildren<TextMeshProUGUI>().text = "I'm looking for an employee for some assistance";
            }
            
        }

        if ((Labnet.isGrabbed || ITS.isGrabbed) && questionAsked)
        {
            if ((answer == "LabNet" && Labnet.isGrabbed) || (answer == "ITS" && ITS.isGrabbed))
            {
                questions.GetComponentInChildren<TextMeshProUGUI>().text = "Great, thanks";
                MasterController.ScoreModify(1, 1, 0, true, true);
            }
            else
            {
                questions.GetComponentInChildren<TextMeshProUGUI>().text = "Hmm...That doesn't really help.";
                MasterController.ScoreModify(1, -1, 0, false, true);

            }
            questionAsked = false;
            done = true;     
            timer = questionDelay;
        }
            
        timer -= Time.deltaTime;

    }
    /*********************************************************************
     * Once the client is destroyed the ClientManager is informed
     * 
     * ##Detailed 
     * This is meant so that in future iterations, there can be more instances of the the PhoneBasedClient sent to the desk
     * 
     * *******************************************************************/
    private void OnDestroy()
    {
        ClientManager.deskClient = false;
    }

}
