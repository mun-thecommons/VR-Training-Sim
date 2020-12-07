using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// #Script Description
/// This script controls the state (fixed/unbroken) of the library computers
/// This script controls the display of the computers states on the library computers monitors
/// </summary>
public class MonitorController : MonoBehaviour
{
    public Sprite monitorWorkingSprite;
    public Sprite monitorBrokenSprite;

    public GameObject monitorCanvas;

    public AudioClip breakSound;
    public AudioClip fixSound;

    public bool isBroken = false;

    public Image monitorImg;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        monitorImg = monitorCanvas.GetComponent<Image>();
        monitorImg.sprite = monitorWorkingSprite;

    }

    /*
     * The computer is broken as indicated on the screen
     */
    public void Break()
    {
        source.PlayOneShot(breakSound);
        isBroken = true;
        monitorImg.sprite = monitorBrokenSprite;
    }

    /*
     * The computer is fixed, the monitor will appear as has been fixed
     */
    public void Fix()
    {
        source.PlayOneShot(fixSound);
        isBroken = false;
        monitorImg.GetComponent<Image>().sprite = monitorWorkingSprite;
        GameManager.monitorsFixed++;
    }

    /*
     * Fix broken computers by restarting or touching the screen
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand") && isBroken)
        {
            Fix();
        }
    }

}
