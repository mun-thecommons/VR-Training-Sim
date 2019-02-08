using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class QuestionInput : MonoBehaviour {

    private TextMeshProUGUI output;
    public TextMeshProUGUI client_2;
    public TextMeshProUGUI client_2Head;
    public TextMeshProUGUI client_3;
    public TextMeshProUGUI client_3Head;
    private PlayerUIScore playerScore;
    
    public List<string> questions = new List<string> {};
    public List<string> answers = new List<string> {};
    public static List<string> mcQuestions= new List<string> { };
    public static List<string> mcCorrectAnswers = new List<string> { };
    public static List<string[]> mcWrongAnswers = new List<string[]> { };

    public static int correct = 0;
    public static int wrong = 0;
    public static int totalScore = 0;
    public static int scoreDetector = 0;
    public static bool answerCorrect;
    public static bool questionAnswered;
    public bool isScoreShowing = false;

    void Awake()
    {
        // playerScore = GameObject.Find("Canvas").GetComponent<PlayerUIScore>();
        //  playerScore = GameObject.Find("Canvas").gameObject.GetComponent<PlayerUIScore>();
        //GameObject myCanvas = GameObject.Find("Canvas");
        //playerScore = myCanvas.GetComponent<PlayerUIScore>();
        output = gameObject.GetComponent<TextMeshProUGUI>();

        fileParse("questions", "C:\\Users\\iccom\\Documents\\Unity - Jordan\\questions.txt");
        fileParse("answers", "C:\\Users\\iccom\\Documents\\Unity - Jordan\\answers.txt");
        fileParse("mc", "C:\\Users\\iccom\\Documents\\Unity - Jordan\\multipleChoice.txt");

        StartCoroutine(clientQuiz());
        StartCoroutine(mcText());
    }

    IEnumerator clientQuiz()
    {
        while (questions.Count > 0)
        {
            int randomIndex = Random.Range(0, questions.Count);
            output.text = questions[randomIndex];

            yield return new WaitUntil(() => TouchDetection.isGrabbingLabnet || TouchDetection.isGrabbingITS);
            string answer = answers[randomIndex];

            if (evaluateAnswer(answer))
            {
                output.text = "Great, thanks!";
                FindObjectOfType<Audio>().correctSound();
                correct += 1;
                totalScore++;
            }
            else
            {
                output.text = "That doesn't really help";
                FindObjectOfType<Audio>().wrongSound();
                wrong += 1;
                totalScore = totalScore - 1;
            }

            yield return new WaitUntil(() => !TouchDetection.isGrabbingLabnet && !TouchDetection.isGrabbingITS);
            yield return new WaitForSeconds(1);
            questions.RemoveAt(randomIndex);
            answers.RemoveAt(randomIndex);
        }
        output.text = "Have a nice day!";
    }

    bool evaluateAnswer(string answer)
    {
        if (answer == "LabNet" && TouchDetection.isGrabbingLabnet)
        {
            return true;
        }
        else if (answer == "ITS" && TouchDetection.isGrabbingITS)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator HidePlayerScore()
    {
       
        yield return new WaitForSeconds(5f);
        GameObject.Find("Canvas").GetComponent<PlayerUIScore>().TurnScoreOff();
        isScoreShowing = false;

    }

IEnumerator mcText()
    {
        while (mcQuestions.Count > 0)
        {
            yield return new WaitUntil(() => Client_2.askingQuestion || Client_3.askingQuestion);
            int randomIndex = Random.Range(0, mcCorrectAnswers.Count);

            client_2Head.text = "Excuse me, " + mcQuestions[randomIndex];
            client_3Head.text = "Excuse me, " + mcQuestions[randomIndex];

            List<string> output = new List<string> { };

            output.Add(mcCorrectAnswers[randomIndex]);
            output.Add(mcWrongAnswers[randomIndex][0]);
            output.Add(mcWrongAnswers[randomIndex][1]);
            output.Add(mcWrongAnswers[randomIndex][2]);

            List<string> shuffledOutput = shuffle(output);

            client_2.text = "A. " + shuffledOutput[0] + "\n" + "B. " + shuffledOutput[1] + "\n" + "X. " + shuffledOutput[2] + "\n" + "Y. " + shuffledOutput[3];
            client_3.text = "A. " + shuffledOutput[0] + "\n" + "B. " + shuffledOutput[1] + "\n" + "X. " + shuffledOutput[2] + "\n" + "Y. " + shuffledOutput[3];

            yield return new WaitUntil(() => (OVRInput.Get(OVRInput.Button.One) || OVRInput.Get(OVRInput.Button.Two) || OVRInput.Get(OVRInput.Button.Three) || OVRInput.Get(OVRInput.Button.Four)) && (Client_2.askingQuestion || Client_3.askingQuestion ));

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
                GameObject.Find("Canvas").GetComponent<PlayerUIScore>().TurnScoreOn();
                GameObject.Find
               // StartCoroutine(HidePlayerScore());
               
                yield return new WaitUntil(() => !Client_2.askingQuestion);
                yield return new WaitUntil(() => !Client_3.askingQuestion);
            }
            else
            {
                client_2Head.text = "That doesn't really help.";
                client_3Head.text = "That doesn't really help.";
                questionAnswered = false;
                wrong++;
               totalScore=totalScore-1;
                isScoreShowing = true;
                //playerScore.TurnScoreOn();
              // StartCoroutine(HidePlayerScore());
                FindObjectOfType<Audio>().wrongSound();
                yield return new WaitForSeconds(2);
            }

            mcQuestions.RemoveAt(randomIndex);
            mcCorrectAnswers.RemoveAt(randomIndex);
            mcWrongAnswers.RemoveAt(randomIndex);
        }
    }

    void fileParse(string toParse, string filepath)
    {
        StreamReader file = new StreamReader(@filepath);
        string line;
        if (toParse.Equals("answers"))
        {
            while ((line = file.ReadLine()) != null)
            {
                answers.Add(line);
            }
        }
        else if (toParse.Equals("questions"))
        {
            while ((line = file.ReadLine()) != null)
            {
                questions.Add(line);
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

    public List<string> shuffle(List<string> list)
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

  

