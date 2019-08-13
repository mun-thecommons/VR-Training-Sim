using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RobotController : MonoBehaviour
{
    public GameObject player;
    public float robbiePlayerDistance = 2f;

    //Let Robbie Start at The MakerSpace
    private Vector3 robbieStartPostion;
    private bool usbTouchingMessaged = false;
    private bool usbInBoxMessaged = false;

    public static bool isTouchingUSB = false;
    public static bool isInUsbBox = false;

    public TextMeshProUGUI robotCanvasText; 

    void Start()
    {
        robbieStartPostion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayMessage();
    }

    void Teleport()
    {
        transform.position = new Vector3(player.transform.position.x + player.transform.forward.x*robbiePlayerDistance, player.transform.position.y, player.transform.position.z + player.transform.forward.z*robbiePlayerDistance);
        transform.LookAt(player.transform);
    }

    void DisplayMessage()
    {
        if(isTouchingUSB && !usbTouchingMessaged)
        {
            usbTouchingMessaged = true;
            Teleport();
            robotCanvasText.text = "Oh wow, you found a lost usb, now you gotta find the USB Box where you can store it";
        }
        else if (isInUsbBox && !usbInBoxMessaged)
        {
            usbInBoxMessaged = true;
            Teleport();
            robotCanvasText.text = "Good Job! Continue doing rounds...";
        }
    }

}
