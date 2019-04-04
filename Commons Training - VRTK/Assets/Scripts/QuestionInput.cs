using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class QuestionInput : MonoBehaviour
{

    //private TextMeshProUGUI output;
  
    public static GameObject canvas;  //works just fine

    public static List<string> mcQuestions = new List<string> { };
    public static List<string> mcCorrectAnswers = new List<string> { };
    public static List<string[]> mcWrongAnswers = new List<string[]> { };
    public static List<string> questionsArray = new List<string> { };
    public static List<string> answersArray = new List<string> { };
    static public PlayerUIScore canvasScript;
    public static int correct = 0;
    public static int wrong = 0;
    public static int totalScore = 0;
    public static int scoreDetector = 0;
    public static bool answerCorrect;
    public static bool questionAnswered;
    public static bool isScoreShowing = false;
   

    void Awake()
    {
        // playerScore = GameObject.Find("Canvas").GetComponent<PlayerUIScore>();
        //  playerScore = GameObject.Find("Canvas").gameObject.GetComponent<PlayerUIScore>();
        //GameObject myCanvas = GameObject.Find("Canvas");
        //playerScore = myCanvas.GetComponent<PlayerUIScore>();
        // output = gameObject.GetComponent<TextMeshProUGUI>();
        

        FileParse("questions", "C:\\Users\\iccom\\Documents\\Unity - Jordan\\questions.txt");
        FileParse("answers", "C:\\Users\\iccom\\Documents\\Unity - Jordan\\answers.txt");
        FileParse("mc", "C:\\Users\\iccom\\Documents\\Unity - Jordan\\multipleChoice.txt");

       // StartCoroutine(clientQuiz());
       // StartCoroutine(mcText());
    }


    static public IEnumerator FlashPlayerScore()
    {
        PlayerUIScore.TurnScoreOn();
        yield return new WaitForSeconds(5f);
        PlayerUIScore.TurnScoreOff();
        isScoreShowing = false;

    }

   

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    ///OLD MC QUESTIONS LOGIC/////////////////
    /*
    IEnumerator mcText()
    {
        while (mcQuestions.Count > 0)
        {
            yield return new WaitUntil(() => Client_2.askingQuestion || Client_3.askingQuestion); //
            int randomIndex = Random.Range(0, mcCorrectAnswers.Count); //

            client_2Head.text = "Excuse me, " + mcQuestions[randomIndex]; // 
            client_3Head.text = "Excuse me, " + mcQuestions[randomIndex]; //

            List<string> output = new List<string> { };

            output.Add(mcCorrectAnswers[randomIndex]);
            output.Add(mcWrongAnswers[randomIndex][0]);
            output.Add(mcWrongAnswers[randomIndex][1]);
            output.Add(mcWrongAnswers[randomIndex][2]);

            List<string> shuffledOutput = shuffle(output);
            
            TextMeshProUGUI ButtonA = client_2.transform.parent.gameObject.transform.Find("ButtonA").gameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI ButtonB = client_2.transform.parent.gameObject.transform.Find("ButtonB").gameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI ButtonC = client_2.transform.parent.gameObject.transform.Find("ButtonC").gameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI ButtonD = client_2.transform.parent.gameObject.transform.Find("ButtonD").gameObject.GetComponent<TextMeshProUGUI>();
            ButtonA.text = shuffledOutput[0];
            ButtonB.text = shuffledOutput[1];
            ButtonC.text = shuffledOutput[2];
            ButtonD.text = shuffledOutput[3];

           // button1 = gameObject.transform.Find("AnswersCanvas").gameObject.transform.Find("button1");

            yield return new WaitUntil(() => (OVRInput.Get(OVRInput.Button.One) || OVRInput.Get(OVRInput.Button.Two) || OVRInput.Get(OVRInput.Button.Three) || OVRInput.Get(OVRInput.Button.Four)) && (Client_2.askingQuestion || Client_3.askingQuestion));

            string answer = mcCorrectAnswers[randomIndex];

            if ((shuffledOutput[0] == answer && OVRInput.Get(OVRInput.Button.One)) || (shuffledOutput[1] == answer && OVRInput.Get(OVRInput.Button.Two))
               || (shuffledOutput[2] == answer && OVRInput.Get(OVRInput.Button.Three)) || (shuffledOutput[3] == answer && OVRInput.Get(OVRInput.Button.Four)))
            {
                client_2Head.text = "Great, thanks!";
                client_3Head.text = "Great, thanks!";
                questionAnswered = true;
                FindObjectOfType<Audio>().correctSound();
                correct++;
                totalScore++;
                isScoreShowing = true;
                canvas.gameObject.GetComponent<PlayerUIScore>().TurnScoreOn();  //works just fine
                //GameObject.Find
                StartCoroutine(HidePlayerScore());


                yield return new WaitUntil(() => !Client_2.askingQuestion);
                yield return new WaitUntil(() => !Client_3.askingQuestion);
            }
            else
            {
                client_2Head.text = "That doesn't really help.";
                client_3Head.text = "That doesn't really help.";
                questionAnswered = false;
                wrong++;
                totalScore = totalScore - 1;
                isScoreShowing = true;
                //playerScore.TurnScoreOn();
                // StartCoroutine(HidePlayerScore());
                FindObjectOfType<Audio>().wrongSound();
                canvas.gameObject.GetComponent<PlayerUIScore>().TurnScoreOn();   //works just fine

                StartCoroutine(HidePlayerScore());
                yield return new WaitForSeconds(2);
            }

            mcQuestions.RemoveAt(randomIndex);
            mcCorrectAnswers.RemoveAt(randomIndex);
            mcWrongAnswers.RemoveAt(randomIndex);
        }
    }
    */




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
        file.Close();
    }
/*
    public static List<string> shuffle(List<string> list)
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
*/

}



