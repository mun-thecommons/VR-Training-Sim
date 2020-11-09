using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class RobbieController : MonoBehaviour
{

    // Enum for robbie's desired movement action
    public enum RobbieMovement
    {
        Stationary,
        WaitForPlayer,
        FollowPlayer,
        LeadPlayer
    }

    // Struct to store dialog as an audio clip and a string of text
    // The text is displayed on robbiecanvas
    struct RobbieDialog
    {
        public string dialogText;
        public AudioClip dialogAudio;

        public RobbieDialog(string dialogText, AudioClip audio)
        {
            this.dialogText  = dialogText;
            this.dialogAudio = audio;
        }
    }

    List<RobbieDialog> robbieTutorialDialog = new List<RobbieDialog>(); // List of dialog robbie will say during the tutorial
    List<RobbieDialog> robbieLevel1Dialog   = new List<RobbieDialog>(); // List of dialog for level 1
    List<RobbieDialog> robbieLevel2Dialog   = new List<RobbieDialog>(); // List of dialog for level 2

    // Robbie things
    public GameObject robbieCanvas;
    public TextMeshProUGUI robotCanvasText;
    private NavMeshAgent agentRobbie;
    private AudioSource audioRobbie;
    public RobbieMovement robbieMovement = RobbieMovement.Stationary; // What should robbie be doing right now? (wait, follow player, lead player, etc.)

    // Other game objects
    private GameObject player;
    public DoorController makerspaceDoor;
    public GameObject tutorialReam;
    private GameManager gameManager;

    // Vectors
    Vector3 deskLocation;         // The location of the desk in game
    Vector3 robbieDestination;    // Where should robbie either lead the player/wait?
    Vector3 tutorialReamLocation;

    // Start is called before the first frame update
    void Start()
    {
        // Load in all of Robbie's tutorial dialog
        robbieTutorialDialog.Add(new RobbieDialog("Welcome to The Commons!", Resources.Load<AudioClip>("RobbieAudio/wel_1")));
        robbieTutorialDialog.Add(new RobbieDialog("My name is Robbie The Robot and I am going to teach you how to work here.", Resources.Load<AudioClip>("RobbieAudio/wel_2")));
        robbieTutorialDialog.Add(new RobbieDialog("You can look around with the right analog stick.Try it now!", Resources.Load<AudioClip>("RobbieAudio/tut_1")));
        robbieTutorialDialog.Add(new RobbieDialog("Well done! Try moving around with the left analog stick.", Resources.Load<AudioClip>("RobbieAudio/tut_2")));
        robbieTutorialDialog.Add(new RobbieDialog("Great! Next try to pick up this paper stack using the grip button on the side of the controller.",  Resources.Load<AudioClip>("RobbieAudio/tut_3")));
        robbieTutorialDialog.Add(new RobbieDialog("Good job! Press the A button to interact with clients and objects. Try it now!", Resources.Load<AudioClip>("RobbieAudio/tut_4")));
        robbieTutorialDialog.Add(new RobbieDialog("Great job! Now follow me to the Computing Support desk to collect your vest and get started!", Resources.Load<AudioClip>("RobbieAudio/tut_99")));

        // Load in all of Robbie's Level 1 Dialog
        robbieLevel1Dialog.Add(new RobbieDialog("This is the Computing Support Desk. Touch the Red Vest to pick it up and start your shift!", Resources.Load<AudioClip>("RobbieAudio/lv1_1")));
        robbieLevel1Dialog.Add(new RobbieDialog("You are ready to start! Your first task is to set up the desk for the day", Resources.Load<AudioClip>("RobbieAudio/lv1_2")));
        robbieLevel1Dialog.Add(new RobbieDialog("Start by plugging in the two phones at the front of the desk", Resources.Load<AudioClip>("RobbieAudio/lv1_3")));
        robbieLevel1Dialog.Add(new RobbieDialog("Next you need to turn on all the computers. Press the flashing red button next to the Rounds Monitor.", Resources.Load<AudioClip>("RobbieAudio/lv1_4")));
        robbieLevel1Dialog.Add(new RobbieDialog("The last step is to unlock the safe. Touch it to open it up.", Resources.Load<AudioClip>("RobbieAudio/lv1_5")));
        robbieLevel1Dialog.Add(new RobbieDialog("You've finished Level One, great job!", Resources.Load<AudioClip>("RobbieAudio/lv1_99")));

        // Load in all of Robbie's Level 2 Dialog
        robbieLevel2Dialog.Add(new RobbieDialog("It's time to start Level Two, in this level we will be performing printer maintenance", Resources.Load<AudioClip>("RobbieAudio/lv2_1")));

        robbieLevel2Dialog.Add(new RobbieDialog("You've finished Level Two, great job!", Resources.Load<AudioClip>("RobbieAudio/lv2_99")));


        if (GameManager.GetGameState() == GameManager.GameState.Tutorial) { makerspaceDoor.isEnabled = false; } // Lock the player in the makerspace if they haven't completed the tutorial
        player = GameObject.FindGameObjectWithTag("Player");

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        // Turn off the paper ream used for the tutorial and save its position
        tutorialReam.SetActive(false);
        tutorialReamLocation = tutorialReam.transform.position;

        // Get robbie sub-components
        audioRobbie = GetComponent<AudioSource>();
        agentRobbie = GetComponent<NavMeshAgent>();

        deskLocation = new Vector3(-17, transform.position.y, -8.5f); // Location of the CS desk (for Robbie to return to)
    }
 
    // Update is called once per frame
    void Update()
    {
        // Robbie's movement state machine
        switch (robbieMovement)
        {
            case RobbieMovement.Stationary: // Look at the player whem waiting
                Vector3 robbieForward = player.transform.position - transform.position;
                robbieForward.y = 0;
                transform.forward = robbieForward;
                break;
            case RobbieMovement.LeadPlayer:
                if (robbieCanvas.activeSelf) { robbieCanvas.SetActive(false); }
                if (Vector3.Magnitude(transform.position - player.transform.position) >=5) // Wait for the player to catch up if they get too far behind
                {
                    agentRobbie.destination = transform.position;
                }
                else
                {
                    agentRobbie.destination = robbieDestination;
                }
                if (Vector3.Magnitude(transform.position - robbieDestination) <= 1 )
                {
                    robbieMovement = RobbieMovement.WaitForPlayer;
                }
                break;
            case RobbieMovement.WaitForPlayer: // Wait for the player to reach the destination
                robbieForward = player.transform.position - transform.position;
                robbieForward.y = 0;
                transform.forward = robbieForward;
                if (Vector3.Magnitude(transform.position - player.transform.position) <=3)
                {
                    robbieMovement = RobbieMovement.Stationary;
                }
                break;
            case RobbieMovement.FollowPlayer:
                if (robbieCanvas.activeSelf) { robbieCanvas.SetActive(false); }
                if (Vector3.Magnitude(transform.position - player.transform.position) >= 3) // Don't get too close to the player in follow mode
                {
                    agentRobbie.destination = player.transform.position;
                }
                else
                {
                    agentRobbie.destination = transform.position;
                }
                break;
        }

        // Robbie's game state machine based on the GameManager's state
        switch (GameManager.GetGameState())
        {
            default:
                // This should never happen
                break;
            case GameManager.GameState.Tutorial:
                PlayTutorial();
                break;
            case GameManager.GameState.Active:
                if (robbieMovement == RobbieMovement.Stationary) // Wait for robbie to be done moving to progress in the game
                {
                    PlayLevel(GameManager.Level);
                }
                break;
            case GameManager.GameState.Paused:
                break;
        }
    }

    // Get robbie to say a line of dialog by putting the text on the canvas and playing the audio clip
    void Say(RobbieDialog dialog)
    {
        if (!robbieCanvas.activeSelf) { robbieCanvas.SetActive(true); }
        robotCanvasText.text = dialog.dialogText;
        audioRobbie.PlayOneShot(dialog.dialogAudio, 1);
    }

    // Robbie's state machine actions

    InputHandler.ButtonControl? expecetedButton = null;
    InputHandler.AxisControl?   expectedAxis    = null;
    int tutorialLocation = 0;
    bool waitingForInput = false;
    bool waitingForPaper = false;
    void PlayTutorial()
    {
        if (waitingForInput) // Hold the tutorial until the player successfully presses the required button
        {
            bool buttonPressed = false;
            if (expecetedButton != null)
            {
                buttonPressed = InputHandler.GetButton(expecetedButton);
            }
            else if (expectedAxis != null)
            {
                Vector2 axis = InputHandler.GetAxis(expectedAxis);
                buttonPressed = axis.x != 0 && axis.y != 0;
            }

            if (!buttonPressed) { return; }

            expecetedButton = null;
            expectedAxis = null;
            waitingForInput = false;
 
        }
        else if (waitingForPaper) // Hold the tutorial until the player successfully picks up and moves the paper
        {
            if (Vector3.Magnitude(tutorialReam.transform.position - tutorialReamLocation) < 0.1f) { return; }
            waitingForPaper = false;
        }

        if(audioRobbie.isPlaying) { return; } // Don't progress until the audio is done playing

        switch (tutorialLocation)
        {
            case 2: // Look around
                waitingForInput = true;
                expectedAxis = InputHandler.AxisControl.LookAxis;
                break;
            case 3: // Move around
                waitingForInput = true;
                expectedAxis = InputHandler.AxisControl.MoveAxis;
                break;
            case 4: // Pick up paper ream, needs special handling
                tutorialReam.SetActive(true);
                waitingForPaper = true;
                break;
            case 5:
                waitingForInput = true;
                expecetedButton = InputHandler.ButtonControl.InteractButton;
                break;
        }

        if (gameManager.skipTutorial || tutorialLocation >= robbieTutorialDialog.Count) // This is when the tutorial is finished
        {
            // Let the player out of the Makerspace
            makerspaceDoor.isEnabled = true;
            makerspaceDoor.OpenDoor();

            robbieDestination = deskLocation;
            GameManager.SetGameState(GameManager.GameState.Active);
            robbieMovement = RobbieMovement.LeadPlayer;
            Destroy(tutorialReam);
            return;
        }

        Say(robbieTutorialDialog[tutorialLocation++]);

    }

    void PlayLevel(int levelNumber)
    {
        switch (levelNumber)
        {
            case 1:
                Level1Handler();
                break;
            case 2:
                Level2Handler();
                break;
        }
    }

    int levelLocation = 0;
    void Level1Handler()
    {
        switch (levelLocation)
        {
            case 1: // Waiting for red vest
                if(!GameManager.hasVest) { return; } // Wait until player gets the vest
                break;
            case 2: // Just dialog...
                break;
            case 3: // Plug in the phones
                if(!GameManager.phonesPluggedIn) { return; } // Wait until all phones are plugged in
                break;
            case 4: // Turn on computers
                if (!GameManager.monitorsOn) { return; }
                break;
            case 5: // Unlock safe
                if (!GameManager.safeOpen) { return; }
                break;
        }

        if (audioRobbie.isPlaying) { return; } // Don't progress until the audio is done playing
        if (GameManager.playerAudioSource.isPlaying) { return; } // Wait for any player audio to stop playing

        if (levelLocation >= robbieLevel1Dialog.Count) // This is where level 1 ends
        {
            GameManager.LevelComplete();
            levelLocation = 0; // Reset the levelLocation index for level 2
            return;
        }    

        Say(robbieLevel1Dialog[levelLocation++]);
    }
    void Level2Handler()
    { 
        switch (levelLocation)
        {
            case 1:     // Dialog
                break;
        }

        if (audioRobbie.isPlaying) { return; } // Don't progress until the audio is done playing
        if (GameManager.playerAudioSource.isPlaying) { return; } // Wait for any player audio to stop playing

        if (levelLocation >= robbieLevel2Dialog.Count) // This is where level 2 ends
        {
            GameManager.LevelComplete();
            levelLocation = 0; // Reset the levelLocation index for next level
            return;
        }

        Say(robbieLevel2Dialog[levelLocation++]);
    }

}
