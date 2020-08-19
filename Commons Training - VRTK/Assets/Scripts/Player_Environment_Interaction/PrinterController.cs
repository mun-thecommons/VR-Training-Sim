using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// #Script Description
/// The script controls the state (full/empty) of the printer game objects and the action of loading paper into the printers
/// </summary>
public class PrinterController : MonoBehaviour
{
    public static int numOfPrintersFilled = 0;
    private bool full = false;
    public TextMeshProUGUI output;
    private const float resetTimer = 30f;
    private float timer = 0;

    private AudioSource singlePrinterFilled;

    private void Start()
    {
        singlePrinterFilled = gameObject.GetComponent<AudioSource>();
        output.text = "Empty";
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(full && timer<=0)
        {
            full = false;
            output.text = "Empty";
        }
        if (numOfPrintersFilled >= 2 && Level.level == 1)
        {
            Level.level1Printer = true;
        }
    }

    /*
     * This function enables the task of loading paper into printers
     * Tha paper game object disappears when being triggered by an unfull printer game object
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paper") && !full)
        {
            output.text = "Full";
            singlePrinterFilled.Play();
            full = true;
            Destroy(other.gameObject);
            timer = resetTimer;
            MasterController.ScoreModify(1, 0, 0, true, false);
            numOfPrintersFilled++;
        }
    }
}
