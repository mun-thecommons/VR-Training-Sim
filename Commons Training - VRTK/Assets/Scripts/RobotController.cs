using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RobotController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    public static bool isTouchingUSB;
    public static bool isInUsbBox;
    public GameObject robotCanvas;
   // private Image speechBubble;
    private float timer = 5f;

    void Start()
    {
       // speechBubble = GetComponentInChildren<Image>();
   
        isTouchingUSB = false;
        isInUsbBox = false;

        robotCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "Hello There! I'm Robbie, the android assigned to guide you through this complex ";
    }

    // Update is called once per frame
    void Update()
    {
        Speech();
        //display robot canvas with tips about USB
        if (isTouchingUSB==true)
        {
            robotCanvas.SetActive(true);
            robotCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "Oh wow, you found a lost usb, now you gotta find the USB Box where you can store it";    
        }

        if (isInUsbBox==true)
        {
            robotCanvas.SetActive(true);
            robotCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "Good Job! Continue doing rounds...";
            isInUsbBox = false;
        }

       
    }

    void Speech()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && robotCanvas.activeSelf)
        {
            robotCanvas.SetActive(false);
            
        }
        if (timer <= 0 && !robotCanvas.activeSelf)
        {
            if ((isTouchingUSB == true || isInUsbBox == true) && timer <= 0)
            {
                timer = 5f;
            }
            
        }
    }
}
