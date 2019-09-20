using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RobotController : MonoBehaviour
{
    public GameObject player;
    public float robbiePlayerDistance = 2f;

    private AudioSource source;
    public AudioClip welcomeAudio;
    public AudioClip level1CompleteAudio;
    public AudioClip level2CompleteAudio;
    public AudioClip level3CompleteAudio;

    public float volume = .25f;

    //Let Robbie Start at The MakerSpace
    private Vector3 robbieStartPostion;
    private bool usbTouchingMessaged = false;
    private bool usbInBoxMessaged = false;
    private bool level1Messaged = false;
    private bool level1CompleteMessage = false;
    private bool level2CompleteMessage = false;
    private bool level3CompleteMessage = false;

    public static bool isTouchingUSB = false;
    public static bool isInUsbBox = false;

    public TextMeshProUGUI robotCanvasText; 

    void Start()
    {
        robbieStartPostion = transform.position;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //DisplayMessage();
        DisplayLevelMessage();
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
            robotCanvasText.text = "Good Job! Continue doing the tasks to finish the level";
        }
    }

    void DisplayLevelMessage()
    {
        if (Level.level == 1 && Level.level1Vest && !Level.level1Printer && !Level.level1USB && !level1Messaged)
        {
            Teleport();
            robotCanvasText.text = "Welcome! Begin by filling the printers with paper and collecting a lost USB stick!";
            level1Messaged = true;
            source.PlayOneShot(welcomeAudio, volume);
        }

        else if(Level.level == 2 && Level.level1Vest && Level.level1Printer && Level.level1USB && !level1CompleteMessage)
        {
            robotCanvasText.text = "Great job! Next, help a client, pick up some garbage, and do the rounds!";
            Teleport();
            level1CompleteMessage = true;
            source.PlayOneShot(level1CompleteAudio, volume);
        }
        else if(Level.level == 3 && Level.level2Client && Level.level2Trash && Level.level2Round && !level2CompleteMessage)
        {
            robotCanvasText.text = "You're almost done! Now, fix a monitor in the lab, answer more questions, and help someone at the desk.";
            Teleport();
            level2CompleteMessage = true;
            source.PlayOneShot(level2CompleteAudio, volume);
        }
        else if (Level.level == 4 && !level3CompleteMessage)
        {

            robotCanvasText.text = "You saved the world and made Aaron proud, not bad!";
            Teleport();
            level3CompleteMessage = true;
            source.PlayOneShot(level3CompleteAudio, volume);
        }           
    }

}
