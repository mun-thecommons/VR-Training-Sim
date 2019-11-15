using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MCClientInteraction : MonoBehaviour
{
    public GameObject clientManager;
    public GameObject player;
    public Canvas questionsCanvas;

    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }
    void Update()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = MasterController.vestCollected ? "Hit A to interact" : "Stop bothering me";
        checkClients();
    }

    void checkClients()
    {
        foreach (Transform child in clientManager.transform)
        {
            if(!(child.GetComponent<MCQuestions>() != null || child.GetComponent<CashClient>() != null || child.GetComponent<PhoneBasedQuestions>() != null))
            {
                continue;
            }
            if(Vector3.Distance(child.position, player.transform.position) < 3f & !questionsCanvas.enabled)
            {
                canvas.enabled = true;
                return;
            }
        }
        canvas.enabled = false;
    }
}
