using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Client_2Text : MonoBehaviour {

    private TextMeshProUGUI clientQuestion;

    List<string> questions = new List<string> { "Answer is A", "Answer is B", "Answer is X", "Answer is Y" };
    List<string> answers = new List<string> { "A", "B", "X", "Y" };
	
	void Start ()
    {
        clientQuestion = gameObject.GetComponent<TextMeshProUGUI>();
        StartCoroutine(multipleChoice());
    }

    IEnumerator multipleChoice()
    {
        yield return new WaitUntil(() => Client_2.askingQuestion);
        int randomIndex = Random.Range(0, questions.Count);
        clientQuestion.text = "Excuse me, " + questions[randomIndex];

        yield return new WaitUntil(() => OVRInput.Get(OVRInput.Button.One) || OVRInput.Get(OVRInput.Button.Two)|| OVRInput.Get(OVRInput.Button.Three) || OVRInput.Get(OVRInput.Button.Four));

        string answer = answers[randomIndex];

        if (evaluateAnswer(answer))
        {
            clientQuestion.text = "Great, thanks!";
        }
        else
            clientQuestion.text = "That doesn't really help";
    }

    bool evaluateAnswer(string answer)
    {
        if (answer == "A" && OVRInput.Get(OVRInput.Button.One))
        {
            return true;
        }
        else if (answer == "B" && OVRInput.Get(OVRInput.Button.Two))
        {
            return true;
        }
        else if (answer == "X" && OVRInput.Get(OVRInput.Button.Three))
        {
            return true;
        }
        else if (answer == "Y" && OVRInput.Get(OVRInput.Button.Four))
        {
            return true;
        }
        else
            return false;
    }
}
