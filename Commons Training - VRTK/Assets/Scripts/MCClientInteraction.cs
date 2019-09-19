using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MCClientInteraction : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = MasterController.vestCollected ? "Hit A to interact" : "Stop bothering me";
    }
}
