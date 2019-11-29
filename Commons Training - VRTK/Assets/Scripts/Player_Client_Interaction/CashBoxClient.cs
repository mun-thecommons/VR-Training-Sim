using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CashBoxClient : MonoBehaviour
{

    private Client client;
    public GameObject answers;
    public GameObject questions;
    public GameObject button1;
    public GameObject button2;
    public GameObject player;
    private CampusCard campusCard;
    private string correctAnswer;
    private bool questionAsked = false;
    public bool questionAnswered = false;
    private bool done = false;
    private float timer = 0f;

    void Start() {
        client = GetComponent<Client>();
        campusCard = GetComponentInChildren<CampusCard>();
        player = GameObject.FindGameObjectWithTag("Player");

        questions.SetActive(false);
        answers.SetActive(false);
    }

    void Update() {
        if (client.askingQuestion && !questionAsked && timer <= 0 && !done)
        {
            questions.GetComponentInChildren<TextMeshProUGUI>().text = "Excuse me, can you check if there is an issue with my campus card? Please try my card at the cash box.";

            button1.GetComponentInChildren<TextMeshProUGUI>().text = "The campus card is expired";
            button2.GetComponentInChildren<TextMeshProUGUI>().text = "The campus card works and the cashbox displays a balance";

            questionAsked = true;
        }

        if (questionAnswered && questionAsked)
        {
            if ((campusCard.expired && button1.GetComponent<ButtonPress>().beingPressed) || (!campusCard.expired && button2.GetComponent<ButtonPress>().beingPressed))

            {
                questions.GetComponentInChildren<TextMeshProUGUI>().text = "Great, thanks";
                MasterController.ScoreModify(1, 1, 0, true, true);
            }
            else
            {
                questions.GetComponentInChildren<TextMeshProUGUI>().text = "Hmm...That doesn't really help.";
                MasterController.ScoreModify(1, -1, 0, false, true);
            }
            answers.SetActive(false);
            questionAsked = false;
            questionAnswered = false;
            done = true;
            Destroy(gameObject, 10f);
        }
        timer -= Time.deltaTime;
        CheckButton();
        CheckRange();
    }
    private void CheckButton()
    {
        if (button1.GetComponent<ButtonPress>().beingPressed || button2.GetComponent<ButtonPress>().beingPressed)
        {
            questionAnswered = true;
        }
    }

    private void CheckRange()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 5f)
        {
            questions.SetActive(true);
            answers.SetActive(true);
            client.askingQuestion = true;
        }
        else
        {
            questions.SetActive(false);
            answers.SetActive(false);
            client.askingQuestion = false;
        }
    }
}