using System.IO;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using OVRTouchSample;
using System.Text.RegularExpressions;


public class MCQuestions : MonoBehaviour {
    private Client client;
    public GameObject greetings;

    private List<string> output = new List<string> { };
    private GameObject player;
    private string correctAnswer;
    private int randomIndex;
    private int currentlySelected;
    private bool questionAsked = false;
    private bool questionAnswered = false;
    private bool done = false;
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
    

    void Start()
    {

        buttonArray = GameObject.FindGameObjectsWithTag("MCButtons");
        questions = GameObject.FindGameObjectWithTag("MCQuestion");

        client = GetComponent<Client>();
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        greetings.SetActive(true); // activate the question Canvas

        greetings.GetComponentInChildren<TextMeshProUGUI>().text = "Hey dude I need help, Press A come on";

        //MC questions canvas
        mcQuestionsCanvas = GameObject.Find("MCQuestionsCanvas").GetComponent<Canvas>();
        mcQuestionsCanvas.enabled = false;
    }

    void Update() {

        //If near client, press A, the client starts asking questions
        if (OVRInput.GetDown(OVRInput.RawButton.A) && Vector3.Distance(transform.position, player.transform.position) < 5f && !client.askingQuestion)
        {
            client.askingQuestion = true;
        }

        //Toggling between answer
        if (client.askingQuestion && !questionAsked)
        {
            player.GetComponent<OVRPlayerController>().enabled = false;
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

            //question has been asked
            questionAsked = true;
        }

        //Now select the answer
        if (questionAsked)
        {
            buttonArray[currentAnswerIndex].GetComponent<Image>().color = Color.white;
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
        
        if(OVRInput.GetDown(OVRInput.RawButton.A) && questionAsked && !questionAnswered)
        {
            questionAnswered = true;
            player.GetComponent<OVRPlayerController>().enabled = true;
            mcQuestionsCanvas.enabled = false;
        }

    } 

    
    //Shuffle function
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
