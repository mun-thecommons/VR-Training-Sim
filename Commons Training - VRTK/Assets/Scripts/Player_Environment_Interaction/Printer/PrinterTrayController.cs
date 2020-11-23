using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterTrayController : MonoBehaviour
{
    private GameObject printer;
    private PrinterController printerController;

    private void Start()
    {
        printerController = transform.parent.gameObject.GetComponent<PrinterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paper"))
        {
            printerController.isFilled = true;
            //printerController.statusText.text = "Full";
        }
    }
}
