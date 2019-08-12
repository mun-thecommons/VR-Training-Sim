using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RobotController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    public static bool isTouchingUSB = false;
    public static bool isInUsbBox = false;
    public GameObject robotCanvas;
   // private Image speechBubble;
    private float timer = 10f;
    private float resetTimer = 10f;

    void Start()
    {
       // speechBubble = GetComponentInChildren<Image>();
        robotCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "Hello There! I'm Robbie, the android assigned to guide you through this complex ";
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        Speech();
        DisplayMessage();
        //display robot canvas with tips about USB
        
    }

    void Speech()
    {
        if(timer <= 0)
        {
            if (robotCanvas.activeSelf)
            {
                robotCanvas.SetActive(false);
            }
            else if(isTouchingUSB || isInUsbBox)
            {
                timer = resetTimer;
            }
        }
    }

    void DisplayMessage()
    {
        if (isTouchingUSB)
        {
            robotCanvas.SetActive(true);
            robotCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "Oh wow, you found a lost usb, now you gotta find the USB Box where you can store it";
        }

        if (isInUsbBox)
        {
            robotCanvas.SetActive(true);
            robotCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "Good Job! Continue doing rounds...";
            //StartCoroutine(Wait());
            isInUsbBox = false;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(resetTimer);
    }
}
