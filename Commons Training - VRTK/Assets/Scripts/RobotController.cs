using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RobotController : MonoBehaviour
{
    public GameObject player;
    public float robbiePlayerDistance = 2f;

    // Tutorial Variables
    private bool movement;
    private bool mainMenu;
    private bool direction;
    private bool staplerShot;
    private bool clientInteract;
    private bool tutorialFinished;


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
        // Used for Tutorial
        movement = false;                   //Bool checklist for the player to complete
        mainMenu = false;
        direction = false;
        staplerShot = false;
        clientInteract = false;
        // End

        robbieStartPostion = transform.position;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //DisplayMessage();

        //Tutorial portion
        if (!tutorialFinished)
        {
            DisplayTutorialMessage();               // Setup as if's to ensure the player must complete tutorial first before level
            CheckTutorialControls();                // scripts begin to show up on the canvas
        }

        else
        {
            MasterController.EnableMovement();
            DisplayLevelMessage();
        }
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

    void DisplayTutorialMessage() 
    {   
        /*Will run through what Bool's are false to determine next message to display
            Should run Chronilogically if i set it up correctly
          */
        if (movement)
        {
            MasterController.DisableMovement();
        }
        if (movement == false)
        {
            robotCanvasText.text = "Welcome to your first Tutorial. To start, use the Left Joystick to control your movement. Go ahead, try it!";
        }
        else if ( direction == false && movement)
        {
            robotCanvasText.text = "Good job, now use the Rigt Joystick to change your direction. Go ahead, try it!";
        }
        else if (mainMenu == false && movement && direction)
        {
            robotCanvasText.text = "Look at you, you'll be a pro in no time. Now press in on the Left Joystick to bring up your Main Menu. To exit simply press the button again.";
        }
        else if (staplerShot == false && movement && direction && mainMenu)
        {
            robotCanvasText.text = "While aboard the ship you may need to throw staplers to achieve certain goals. To do this press down on the B button on the right controller.";
        }
        else if (clientInteract == false && movement && direction && mainMenu && staplerShot)
        {
            robotCanvasText.text = "Finally, while aboard the ship you'll notice some people wil be looking for you're help. These individuals will have ! above their heads. To help these people press the A button.";
        }

    }

    void CheckTutorialControls()
    {/* Checks what is being displayed on robbie's canvas to determine which Input.Getbuttondown to look for.
        After detecting the correct button has been pressed, will set the apropriate bool to true and hence
        set the TutorialMessage to the next step.
         */
        if (robotCanvasText.text == "Welcome to your first Tutorial. To start, use the Left Joystick to control your movement. Go ahead, try it!" )
        {
            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft)) 
            {
                movement = true;
            }
        }
        if (robotCanvasText.text == "Good job, now use the Rigt Joystick to change your direction. Go ahead, try it!")
        {
            if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft))           
            {
                direction = true;
            }
        }
        if (robotCanvasText.text == "Look at you, you'll be a pro in no time. Now press in on the Left Joystick to bring up your Main Menu. To exit simply press the button again.")
        {
            if (OVRInput.GetDown(OVRInput.RawButton.LThumbstick))
            {
                mainMenu = true;
            }
        }
        if (robotCanvasText.text == "While aboard the ship you may need to throw staplers to achieve certain goals. To do this press down on the B button on the right controller.")
        {
            if (OVRInput.GetDown(OVRInput.RawButton.B))
            {
                staplerShot = true;
            }
        }
        if (robotCanvasText.text == "Finally, while aboard the ship you'll notice some people wil be looking for you're help. These individuals will have ! above their heads. To help these people press the A button.")
        {
            if (OVRInput.GetDown(OVRInput.RawButton.A))
            {
                clientInteract = true;
                tutorialFinished = true;
            }
        }
    }

}
