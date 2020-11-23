using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrinterController : MonoBehaviour
{

    public GameObject printerTray;
    public GameObject printerPaper;
    public GameObject fillIndicator;
    public GameObject printer;

    private Renderer printerRenderer;

    private Vector3 trayStartLocation;
    private Quaternion trayRotation;
    private float trayOpenPos = 0.4f;
    private bool trayOpen = false;

    private float fillIndicatorEmptyPos = -0.025f;


    public bool isFilled = false;

    // Start is called before the first frame update
    void Start()
    {
        printerRenderer = printer.GetComponent<Renderer>();
        printerRenderer.materials[4].color = Color.red;
    }

    private void Update()
    {
        trayOpen = printerTray.transform.localPosition.z >= trayOpenPos / 2;

        if (isFilled != printerPaper.activeSelf) // Fill state has changed
        { 
            printerPaper.SetActive(isFilled); // Set paper visibility accordingly

            if (isFilled)
            {
                fillIndicator.transform.localPosition = new Vector3(fillIndicator.transform.localPosition.x, 0, fillIndicator.transform.localPosition.z);
                printerRenderer.materials[4].color = Color.green; // Color Printer LCD for extra indication
            }
            else 
            {
                fillIndicator.transform.localPosition = new Vector3(fillIndicator.transform.localPosition.x, fillIndicatorEmptyPos, fillIndicator.transform.localPosition.z);
                printerRenderer.materials[4].color = Color.red;
            }
        }

        if (printerTray.transform.localPosition.z > trayOpenPos) // Is the tray opened too far? 
        {
            printerTray.transform.localPosition = new Vector3(printerTray.transform.localPosition.x, printerTray.transform.localPosition.y, trayOpenPos); 
        }
        else if (printerTray.transform.localPosition.z < trayStartLocation.z) // Is the tray closed too far?
        {
            printerTray.transform.localPosition = new Vector3(printerTray.transform.localPosition.x, printerTray.transform.localPosition.y, trayStartLocation.z); 
        }

        if (printerTray.transform.localPosition.x != trayStartLocation.x || printerTray.transform.localPosition.y != trayStartLocation.y) // Is the tray moved in a direction it shouldn't be?
        { 
            printerTray.transform.localPosition = new Vector3(trayStartLocation.x, trayStartLocation.y, printerTray.transform.localPosition.z); 
        }
        if (printerTray.transform.rotation != trayRotation) { printerTray.transform.rotation = trayRotation; } // Has the tray been rotated?
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paper") && trayOpen && !isFilled)
        {
            if (GameManager.hasVest)
            {
                isFilled = true;
                Destroy(other.gameObject);
                GameManager.playerAudioSource.PlayOneShot(GameManager.successAudioClip);
            }
            else
            {
                GameManager.playerAudioSource.PlayOneShot(GameManager.deniedAudioClip);
            }

        }
    }
}
