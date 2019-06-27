using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class QuestionInput : MonoBehaviour
{
    public TextAsset questions;
    public TextAsset answers;
    public TextAsset mc;
    public TextAsset cashbox;


    public static GameObject canvas;  //works just fine

    public static List<string> mcQuestions = new List<string> { };
    public static List<string> mcCorrectAnswers = new List<string> { };
    public static List<string[]> mcWrongAnswers = new List<string[]> { };
    public static List<string> questionsArray = new List<string> { };
    public static List<string> answersArray = new List<string> { };
    public static List<string> mcAudio = new List<string> { };

    //forcashBoxClient
    public static List<string> cashBoxQuestions = new List<string> { };
    public static List<string> cashBoxCorrectAnswers = new List<string> { };
    public static List<string[]> cashBoxWrongAnswers = new List<string[]> { };

    static public PlayerUIScore canvasScript;
    public static int correct = 0;
    public static int wrong = 0;
    //public static int totalScore = 0;
    public static int techScore = 0;
    public static int custServScore = 0;
    public static int profScore = 0;
    public static int scoreDetector = 0;
    public static bool answerCorrect;
    public static bool questionAnswered;
    public static Audio audio;
    

    void Awake()
    {
        FileParse("questions", questions);
        FileParse("answers", answers);
        FileParse("mc", mc);
        FileParse("cashboxQuestions", cashbox);
        audio = FindObjectOfType<Audio>();
    }

    public static void ScoreModify(int prof, int cs, int tech, bool correct, bool playSound)
    {
        profScore += prof;
        custServScore += cs;
        techScore += tech;
        if(playSound)
        {
            if(correct)
            {
                audio.correctSound();
            }
            else
            {
                audio.wrongSound();
            }
        }
    }

    void FileParse(string toParse, TextAsset textFile)
    {
        string[] fLines = textFile.text.Split("\n"[0]);
        if (toParse.Equals("answers"))
        {
            foreach(string line in fLines)
            {
                answersArray.Add(line);
            }
        }
        else if (toParse.Equals("questions"))
        {
            foreach (string line in fLines)
            {
                questionsArray.Add(line);
            }
        }
        else if (toParse.Equals("mc"))
        {
            for (int i = 0; i+5 < fLines.Length; i += 6)
            {
                mcQuestions.Add(fLines[i]);
                mcCorrectAnswers.Add(fLines[i + 1]);
                string[] inanswers = { fLines[i + 2], fLines[i + 3], fLines[i + 4] };
                mcWrongAnswers.Add(inanswers);
                mcAudio.Add(fLines[i + 5]);
            }
        }
        else if (toParse.Equals("cashboxQuestions"))
        {
            for(int i = 0; i+4 < fLines.Length; i+=5)
            {
                cashBoxQuestions.Add(fLines[i]);
                cashBoxCorrectAnswers.Add(fLines[i+1]);
                string[] inCashBoxAnswers = { fLines[i+2], fLines[i+3], fLines[i+4] };
                cashBoxWrongAnswers.Add(inCashBoxAnswers);
            }
        }
    }
}



