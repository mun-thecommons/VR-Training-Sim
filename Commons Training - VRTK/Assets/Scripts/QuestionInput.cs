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

    public static List<string> mcQuestions = new List<string> { };
    public static List<string> mcCorrectAnswers = new List<string> { };
    public static List<string[]> mcWrongAnswers = new List<string[]> { };
    public static List<string> questionsArray = new List<string> { };
    public static List<string> answersArray = new List<string> { };
    public static List<string> mcAudio = new List<string> { };
    public static List<string> cashBoxQuestions = new List<string> { };
    public static List<string> cashBoxCorrectAnswers = new List<string> { };
    public static List<string[]> cashBoxWrongAnswers = new List<string[]> { };
  
    void Awake()
    {
        FileParse("questions", questions);
        FileParse("answers", answers);
        FileParse("mc", mc);
        FileParse("cashboxQuestions", cashbox);
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



