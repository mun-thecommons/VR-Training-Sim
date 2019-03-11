using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPress : MonoBehaviour
{
    public bool beingPressed = false;
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            beingPressed = true;
            GetComponentInParent<MCQuestions>().questionAnswered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            beingPressed = false;
        }
    }

}
