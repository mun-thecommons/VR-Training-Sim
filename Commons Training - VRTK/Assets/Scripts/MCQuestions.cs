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
    public GameObject questions;
    public GameObject answers;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    private List<string> output = new List<string> { };
    private GameObject player;
    private string correctAnswer;
    private int randomIndex;
    private int currentlySelected;
    private bool questionAsked = false;
    public bool questionAnswered = false;
    private bool done = false;
    public float questionDelay = 10f;
    private float timer = 5f;
    private float resetTimer = 5f;
    private AudioSource audioSource;
    private string pathStart = "Audio/";
    private AudioClip questionAudio;
    static private GameObject[] buttonArray;
    private Canvas mcQuestionsCanvas;
    

    void Start()
    {
        buttonArray = GameObject.FindGameObjectsWithTag("MCButtons");
        client = GetComponent<Client>();
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        questions.SetActive(true); // activate the question Canvas
        answers.SetActive(false); //deactive answer Canvas permanently
        questions.GetComponentInChildren<TextMeshProUGUI>().text = "Hey dude I need help, Press A come on";

        //MC questions canvas
        mcQuestionsCanvas = GameObject.Find("MCQuestionsCanvas").GetComponent<Canvas>();
        mcQuestionsCanvas.enabled = false;

        buttonArray[0].GetComponent<Image>().color = Color.red;
    }

    void Update() {
        if (OVRInput.GetDown(OVRInput.RawButton.A) && Vector3.Distance(transform.position, player.transform.position) < 5f)
        {
            client.askingQuestion = !client.askingQuestion;
        }

        if (client.askingQuestion)
        {
            //GameObject.Find("OVRPlayerController").SetActive(false); //try this out later
            player.GetComponent<OVRPlayerController>().enabled = false;
            questions.SetActive(false);
            mcQuestionsCanvas.enabled = true;
            if (buttonArray[0].GetComponent<Image>().color == Color.red)
            {
                if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickRight))
                {
                    buttonArray[0].GetComponent<Image>().color = Color.white;
                    buttonArray[1].GetComponent<Image>().color = Color.red;
                }
                if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickDown))
                {
                    buttonArray[0].GetComponent<Image>().color = Color.white;
                    buttonArray[2].GetComponent<Image>().color = Color.red;
                }
            }
            if (buttonArray[1].GetComponent<Image>().color == Color.red)
            {
                if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickLeft))
                {
                    buttonArray[1].GetComponent<Image>().color = Color.white;
                    buttonArray[0].GetComponent<Image>().color = Color.red;
                }
                if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickDown))
                {
                    buttonArray[1].GetComponent<Image>().color = Color.white;
                    buttonArray[3].GetComponent<Image>().color = Color.red;
                }
            }
            if (buttonArray[2].GetComponent<Image>().color == Color.red)
            {
                if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickUp))
                {
                    buttonArray[2].GetComponent<Image>().color = Color.white;
                    buttonArray[0].GetComponent<Image>().color = Color.red;
                }
                if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickRight))
                {
                    buttonArray[2].GetComponent<Image>().color = Color.white;
                    buttonArray[3].GetComponent<Image>().color = Color.red;
                }
            }
            if (buttonArray[3].GetComponent<Image>().color == Color.red)
            {
                if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickUp))
                {
                    buttonArray[3].GetComponent<Image>().color = Color.white;
                    buttonArray[1].GetComponent<Image>().color = Color.red;
                }
                if (OVRInput.GetDown(OVRInput.RawButton.LThumbstickLeft))
                {
                    buttonArray[3].GetComponent<Image>().color = Color.white;
                    buttonArray[2].GetComponent<Image>().color = Color.red;
                }
            }
        }


        /*
        if (questionAnswered && questionAsked)
        {
            if ((output[0] == correctAnswer && button1.GetComponent<ButtonPress>().beingPressed) || (output[1] == correctAnswer && button2.GetComponent<ButtonPress>().beingPressed) ||
                (output[2] == correctAnswer && button3.GetComponent<ButtonPress>().beingPressed) || (output[3] == correctAnswer && button4.GetComponent<ButtonPress>().beingPressed))
            {
                questions.GetComponentInChildren<TextMeshProUGUI>().text = "Great, thanks";
                MasterController.ScoreModify(1, 1, 0, true, true);
            }

            else
            {
                questions.GetComponentInChildren<TextMeshProUGUI>().text = "Hmm...That doesn't really help.";
                MasterController.ScoreModify(1, -1, 0, false, true);
            }

            QuestionInput.mcQuestions.RemoveAt(randomIndex);
            QuestionInput.mcCorrectAnswers.RemoveAt(randomIndex);
            QuestionInput.mcWrongAnswers.RemoveAt(randomIndex);
            answers.SetActive(false);
            questionAsked = false;
            questionAnswered = false;
            done = true;

            Destroy(gameObject, 10f);
        }
        */
    } 

    private void CheckButton()
    {
        if(button1.GetComponent<ButtonPress>().beingPressed || button2.GetComponent<ButtonPress>().beingPressed ||
            button3.GetComponent<ButtonPress>().beingPressed || button4.GetComponent<ButtonPress>().beingPressed) 
        {
            questionAnswered = true;
        }
    }
    
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

    private void SetUpQuestion()
    {
        //Deactive LocalAvatar
        player.GetComponent<OVRPlayerController>().enabled = false;
        //Client is asking question
        client.askingQuestion = true;
        //Active questions and answers Canvas
        questions.SetActive(false);
        answers.SetActive(false);
        mcQuestionsCanvas.enabled = true;

       
        

        /*
        randomIndex = Random.Range(0, QuestionInput.mcCorrectAnswers.Count);
        questions.GetComponentInChildren<TextMeshProUGUI>().text = "Excuse me, " + QuestionInput.mcQuestions[randomIndex];
        output = new List<string> { };

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


        button1.GetComponentInChildren<TextMeshProUGUI>().text = output[0];
        button2.GetComponentInChildren<TextMeshProUGUI>().text = output[1];
        button3.GetComponentInChildren<TextMeshProUGUI>().text = output[2];
        button4.GetComponentInChildren<TextMeshProUGUI>().text = output[3];
        correctAnswer = QuestionInput.mcCorrectAnswers[randomIndex];
        questionAsked = true;
        */
    }

}
