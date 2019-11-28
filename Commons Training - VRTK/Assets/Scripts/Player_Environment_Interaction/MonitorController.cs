using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorController : MonoBehaviour
{
    public Sprite monitorWorkingSprite;
    public Sprite monitorBrokenSprite;

    public GameObject monitorCanvas;

    public AudioClip breakSound;
    public AudioClip fixSound;

    public bool isBroken = false;

    private Image monitorImg;
    private AudioSource source;


    private void Start()
    {
        source = GetComponent<AudioSource>();

        monitorImg = monitorCanvas.GetComponent<Image>();
        monitorImg.sprite = monitorWorkingSprite;
    }

    public void Break()
    {
        source.PlayOneShot(breakSound);
        isBroken = true;
        monitorImg.sprite = monitorBrokenSprite;
    }

    public void Fix()
    {
        source.PlayOneShot(fixSound);
        isBroken = false;
        monitorImg.GetComponent<Image>().sprite = monitorWorkingSprite;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand") && isBroken)
        {
            Fix();
        }
    }

}
