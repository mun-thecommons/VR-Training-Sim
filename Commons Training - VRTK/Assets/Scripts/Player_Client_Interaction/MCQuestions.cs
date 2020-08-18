using System.IO;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;
using System.Text.RegularExpressions;

/// <summary>
/// Contains the logic used by the MCClient (*More then just Question*)
/// 
/// ##Detailed
/// Contains the logic used with interacting with the Question boxes (How to change the player selection). 
/// As well, the logic used for selecting the proper audio to use with each question that may be chosen. If a question does not have a Audio
/// clip that matches it a line will show in the Command line stating "No audio clip assigned for question".
/// </summary>
public class MCQuestions : MonoBehaviour {
    private Client client;
    public GameObject greetings;
    public static int numOfClientsHelped = 0;

   
    private List<string> output = new List<string> { };
    private GameObject player;
    private string correctAnswer;
    private int randomIndex;
    private int currentlySelected;
    private bool questionAnswered = false;
    private bool questionSetup = false;
    public float questionDelay = 10f;
    private float timer = 5f;
    private float resetTimer = 5f;
    private AudioSource audioSource;
    private string pathStart = "Audio/";
    private AudioClip questionAudio;
    private GameObject[] buttonArray;
    private GameObject questions;
    private Canvas mcQuestionsCanvas;
    private int currentAnswerIndex = 0;
    private float y;

    
    void Start()
    {
        buttonArray = GameObject.FindGameObjectsWithTag("MCButtons");
        questions = GameObject.FindGameObjectWithTag("MCQuestion");

        client = GetComponent<Client>();
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        mcQuestionsCanvas = GameObject.FindGameObjectWithTag("MCQuestionCanvas").GetComponent<Canvas>();
        y = transform.rotation.y;
    }

    void Update()
    {
        if(GetComponent<NavMeshAgent>().isStopped && !questionSetup)
        {
            greetings.GetComponent<Canvas>().enabled = true;
            questionSetup = true;
        }
        if (client.askingQuestion)
        {
            ChangeAnswer();
        }
        

        if (Vector3.Distance(transform.position, player.transform.position) < 3f)
        {

            if (OVRInput.GetDown(OVRInput.RawButton.A) && !questionAnswered && questionSetup)
            {
                
                if (!client.askingQuestion && !MasterController.inMenu)
                {
                    AskQuestion();
                    InputHandler.DisableMovement();
                }
                else
                {
                    SelectAnswer();
                    InputHandler.EnableMovement();
                }
            }
        }
        
        if(questionSetup && Vector3.Distance(transform.position, player.transform.position) < 5f)
        {
            Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(targetPosition);
        }
    } 
    /**************************************************************
     * Changes the textbox that the player is highlighting for answering a question
     * 
     * ##Detailed
     * he answer that the player is highlighting will be in red. Based on which box the player is currentlyu highlighting the logic
     * will commence based on if they move the joystick up/down left/right to move ther selection box.
     * 
     * ************************************************************/
    private void ChangeAnswer()
    {
        buttonArray [currentAnswerIndex].GetComponent<Image>().color = Color.white;
        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickRight) || OVRInput.GetDown(OVRInput.RawButton.LThumbstickLeft))
        {
            currentAnswerIndex = currentAnswerIndex == 0 || currentAnswerIndex == 2 ? currentAnswerIndex + 1 : currentAnswerIndex - 1;
        }
        else if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickUp) || OVRInput.GetDown(OVRInput.RawButton.LThumbstickDown))
        {
            currentAnswerIndex = currentAnswerIndex == 0 || currentAnswerIndex == 1 ? currentAnswerIndex + 2 : currentAnswerIndex - 2;
        }
        buttonArray[currentAnswerIndex].GetComponent<Image>().color = Color.red;
    }

    /********************************************************
     * Sets the question canvas to active upon interacting with 'A'
     * 
     * ##Detailed
     * Upon interacting with the Client the player will be given four options to choose between. These four options 
     * will be shuffled each time using the Shuffle function below. Therefore engaging the player in the Multiple Choice question style
     * 
     * *See line 77 for use*
     * 
     * ***********************************************/
    private void AskQuestion()
    {
        client.askingQuestion = true;
        
        MasterController.inMenu = true;
        greetings.SetActive(false);
        mcQuestionsCanvas.enabled = true;

        randomIndex = Random.Range(0, QuestionInput.mcCorrectAnswers.Count);
        questions.GetComponent<TextMeshProUGUI>().text = "Excuse me, " + QuestionInput.mcQuestions[randomIndex];

        output.Add(QuestionInput.mcCorrectAnswers[randomIndex]);
        output.Add(QuestionInput.mcWrongAnswers[randomIndex][0]);
        output.Add(QuestionInput.mcWrongAnswers[randomIndex][1]);
        output.Add(QuestionInput.mcWrongAnswers[randomIndex][2]);
        string audioName = QuestionInput.mcAudio[randomIndex].TrimEnd('\n', '\r');
        if (!audioName.Equals("none"))
        {
            questionAudio = Resources.Load(pathStart + audioName) as AudioClip;
            audioSource.clip = questionAudio;
            audioSource.Play();
        }
        else
        {
            Debug.Log("No audio clip assigned for question");
        }
        output = Shuffle(output);

        buttonArray[0].GetComponentInChildren<TextMeshProUGUI>().text = output[0];
        buttonArray[1].GetComponentInChildren<TextMeshProUGUI>().text = output[1];
        buttonArray[2].GetComponentInChildren<TextMeshProUGUI>().text = output[2];
        buttonArray[3].GetComponentInChildren<TextMeshProUGUI>().text = output[3];
        correctAnswer = QuestionInput.mcCorrectAnswers[randomIndex];
        buttonArray[currentAnswerIndex].GetComponent<Image>().color = Color.red;
    }

    /******************************************************
     * Determines reaction of choosing an answer
     * 
     * ##Detailed
     * If the correct answer is chosen the UMAMoodController is set to the appropriate facial expression (Happy) if incorrect it will be set to Upset. There
     * is also a textbox above the clients that will give a ':)' or ':('.
     * It also interacts with the Level script to keep track of completed tasks. 
     * 
     * *****************************************************/
    private void SelectAnswer()
    {
        questionAnswered = true;
        client.askingQuestion = false;
        player.GetComponent<OVRPlayerController>().enabled = true;
        mcQuestionsCanvas.enabled = false;
        greetings.SetActive(true);

        if (buttonArray[currentAnswerIndex].GetComponentInChildren<TextMeshProUGUI>().text.Equals(correctAnswer))
        {
            greetings.GetComponentInChildren<TextMeshProUGUI>().text = ":)";
            numOfClientsHelped++;
            MasterController.ScoreModify(1, 0, 0, true, false);
            UMAMoodController.mood = 1;
            if (Level.level == 2)
            {
                Level.level2Client = true;
            }

            if (numOfClientsHelped >= 2 && Level.level == 3)
            {
                Level.level3ClientLab = true;
            }


        }
        else
        {
            greetings.GetComponentInChildren<TextMeshProUGUI>().text = ":(";
            MasterController.ScoreModify(-1, 0, 0, false, false);
            UMAMoodController.mood = 2;
        }
        buttonArray[currentAnswerIndex].GetComponent<Image>().color = Color.white;
        MasterController.inMenu = false;
        Destroy(gameObject, 5f);
    }
    
    /*******************************************
     * Used to shuffle an array as to add to randomness
     * 
     * ##Detailed
     * For this script the Shuffle is changing the position that answers will be presented in the 4 
     * choice box positions (Top left, Top right, Bottom left, Bottom right)
     * 
     * *****************************************/
    public static List<string> Shuffle(List<string> list)
    {
        List<string> shuffled = new List<string> { };

        while (list.Count > 0) //Shuffle List
        {
            int randomI = Random.Range(0, list.Count);

            shuffled.Add(list[randomI]);
            list.RemoveAt(randomI);
        }

        return shuffled;
    }


}
