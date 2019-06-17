using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class QuestionInput : MonoBehaviour
{

  
    public static GameObject canvas;  //works just fine

    public static List<string> mcQuestions = new List<string> { };
    public static List<string> mcCorrectAnswers = new List<string> { };
    public static List<string[]> mcWrongAnswers = new List<string[]> { };
    public static List<string> questionsArray = new List<string> { };
    public static List<string> answersArray = new List<string> { };

    //forcashBoxClient
    public static List<string> cashBoxQuestions = new List<string> { };
    public static List<string> cashBoxCorrectAnswers = new List<string> { };
    public static List<string[]> cashBoxWrongAnswers = new List<string[]> { };
    static private float timer = 0f;
    static private float scoreTimer = 5f;

    static public PlayerUIScore canvasScript;
    public static int correct = 0;
    public static int wrong = 0;
    public static int totalScore = 0;
    public static int scoreDetector = 0;
    public static bool answerCorrect;
    public static bool questionAnswered;
    public static bool isScoreShowing = false;
    public static Audio audio;
   

    void Awake()
    {  
        FileParse("questions", "C:\\Users\\iccom\\Documents\\GitHub\\VR-Training-Sim\\Commons Training - VRTK\\Assets\\questionanswers\\questions.txt");
        FileParse("answers", "C:\\Users\\iccom\\Documents\\GitHub\\VR-Training-Sim\\Commons Training - VRTK\\Assets\\questionanswers\\answers.txt");
        FileParse("mc", "C:\\Users\\iccom\\Documents\\GitHub\\VR-Training-Sim\\Commons Training - VRTK\\Assets\\questionanswers\\multipleChoice.txt");

        //for a cashboxclient
        FileParse("mc", "C:\\Users\\iccom\\Documents\\GitHub\\VR-Training-Sim\\Commons Training - VRTK\\Assets\\questionanswers\\cashboxQuestions.txt");
        audio = FindObjectOfType<Audio>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && isScoreShowing)
        {
            PlayerUIScore.TurnScoreOff();
            isScoreShowing = false;
        }
    }

    static public IEnumerator FlashPlayerScore()
    {
        PlayerUIScore.TurnScoreOn();
        yield return new WaitForSeconds(5f);
        PlayerUIScore.TurnScoreOff();
        isScoreShowing = false;
    }

    static public void ScoreIncrement(int optionalVal = 1)
    {
        correct ++;
        totalScore += optionalVal;
        isScoreShowing = true;
        PlayerUIScore.TurnScoreOn();
        audio.correctSound();
        timer = scoreTimer;
    }

    static public void ScoreDecrement(int optionalVal = 1)
    {
        wrong++;
        totalScore -= optionalVal;
        isScoreShowing = true;
        PlayerUIScore.TurnScoreOn();
        audio.wrongSound();
        timer = scoreTimer;
    }

    void FileParse(string toParse, string filepath)

    {
        StreamReader file = new StreamReader(@filepath);
        string line;
        if (toParse.Equals("answers"))
        {
            while ((line = file.ReadLine()) != null)
            {
                answersArray.Add(line);
            }
        }
        else if (toParse.Equals("questions"))
        {
            while ((line = file.ReadLine()) != null)
            {
                questionsArray.Add(line);
            }
        }
        else if (toParse.Equals("mc"))
        {
            while ((line = file.ReadLine()) != null)
            {
                mcQuestions.Add(line);
                mcCorrectAnswers.Add(file.ReadLine());
                string[] inanswers = { file.ReadLine(), file.ReadLine(), file.ReadLine() };
                mcWrongAnswers.Add(inanswers);
            }
        }

        //reading cashboxquestions
        else if (toParse.Equals("cashboxQuestions"))
        {
            while ((line = file.ReadLine()) != null)
            {
                cashBoxQuestions.Add(line);
                cashBoxCorrectAnswers.Add(file.ReadLine());
                string[] inCashBoxAnswers = { file.ReadLine(), file.ReadLine(), file.ReadLine() };
                cashBoxWrongAnswers.Add(inCashBoxAnswers);
            }
        }

        file.Close();
    }


}



