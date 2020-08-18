﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;
/// <summary>
/// This script holds the string messages used for the Questions and Answers of NPC's
/// 
/// ##Detailed
/// This script houses the Questions, Answers, and Incorrect Answers used for the MCClients, PhonebasedClients and **Cashbox Clients**(See note). 
/// 
/// @see PhoneBasedQuestions; MCQuestions
/// @Note *Unsure if the Cashbox client questions are still used as this feature was updated*
/// </summary>
public class QuestionInput : MonoBehaviour
{
    public TextAsset questions;
    public TextAsset answers;
    public TextAsset mc;
    public TextAsset cashbox;

    public static List<string> mcQuestions = new List<string> { };          /*! @brief Questions for the MCClients*/
    public static List<string> mcCorrectAnswers = new List<string> { };     /*! @brief Correct answers used for the MCClients*/
    public static List<string[]> mcWrongAnswers = new List<string[]> { };   /*! @brief Incorrect answers used for the MCClients*/
    public static List<string> questionsArray = new List<string> { };       /*! @brief Questions for the PhoneBasedClient*/
    public static List<string> answersArray = new List<string> { };         /*! @brief Answers for the PhoneBasedClients*/
    public static List<string> mcAudio = new List<string> { };              /*! @brief Unsure if this is still used*/
    public static List<string> cashBoxQuestions = new List<string> { };        /*! @brief Unsure if this is still used*/
    public static List<string> cashBoxCorrectAnswers = new List<string> { };    /*! @brief Unsure if this is still used*/
    public static List<string[]> cashBoxWrongAnswers = new List<string[]> { };  /*! @brief Unsure if this is still used*/

    void Awake()
    {
        FileParse("questions", questions);
        FileParse("answers", answers);
        FileParse("mc", mc);
        FileParse("cashboxQuestions", cashbox); 
    }
    /*******************************
     * Function is used to convert a Text file to an array.
     * 
     * @param toParse This param is used to ensure the correct textfile is chosen for the role. (Answers for answerArray, Questions for questionarray)
     * @param textFile The textfile in which the array is to be created from
     * ##Detailed
     * This function takes a text file and converts it into an array of strings. This looks like an array 
     * of x amount of answers, that align to the array of x amount of questions. Therefore, dependent on how the text file was created the questions and answers can
     * line up correctly to have the same spot within the arrays so that questionArray[3]'s corresponding answer is within answersArray[3]. 
     * 
     * *****************************/
    void FileParse(string toParse, TextAsset textFile)
    {
        string[] fLines = textFile.text.Split("\n"[0]);
        if (toParse.Equals("answers"))
        {
            foreach(string line in fLines)
            {
                string str= line.TrimEnd('\n', '\r');
                answersArray.Add(str);
            }
        }
        else if (toParse.Equals("questions"))
        {
            foreach (string line in fLines)
            {
                string str = line.TrimEnd('\n', '\r');
                questionsArray.Add(str);
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



