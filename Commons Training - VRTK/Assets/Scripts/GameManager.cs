using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;



/// <summary>
/// Control the procession of the game
/// 
/// ##Detailed
/// This script (and the GameManager object that it is attached to) is responsible for managing the game state.
/// 
/// ### Game States
/// TODO
/// 
/// </summary>
public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        Tutorial,
        Active,
        Paused
    }

    public bool skipTutorial = false; // Skip the game tutorial


    public GameObject libraryRoof;

    public AudioSource playerAudio;
    public static AudioSource playerAudioSource; // Map audio source to a static variable to make it easier to play audio focused on player

    public AudioClip successAudio;
    public static AudioClip successAudioClip;

    public AudioClip deniedAudio;
    public static AudioClip deniedAudioClip;

    public AudioClip luAudio;
    public static AudioClip levelUpAudio;

    private GameObject robbie;
    private RobbieController robbieController;

    private GameObject player;
    private static ConfettiController confettiController;

    public static GameState gameState;
    public static int Level;

    public int startingLevel = 2;
    public float vol = 0.5f;


    public static List<string> watchedVideos = new List<string>();
    public static bool gameOver = false;

    // Level one state variables
    public static bool hasVest = false;
    public static bool phonesPluggedIn = false;
    public static bool monitorsOn = false;
    public static bool safeOpen = false;

    // Level two state variables
    public static bool printersFilled = false;

    // Level three state variables
    public static bool roundsNeeded = true;
    public static bool monitorsBroken = false;
    public static int monitorsFixed = 0;
    public static int trashCollected = 0;
    public static int scrapPaperCollected = 0;
    public static int coinsCollected = 0;



    // Use awake instead of start to set all game parameters before starting anything else
    void Awake()
    {
        // Start the tutorial or the main game?
/*        if (skipTutorial) { gameState = GameState.Active; }
        else { gameState = GameState.Tutorial; }*/
        gameState = GameState.Tutorial;

        libraryRoof.SetActive(true); // Enable the roof. It's disabled usually so that the developer can see into the library while working on it

        robbie = GameObject.FindGameObjectWithTag("Robbie");
        robbieController = robbie.GetComponent<RobbieController>();

        player = GameObject.FindGameObjectWithTag("Player");

        playerAudioSource = playerAudio;
        successAudioClip  = successAudio;
        deniedAudioClip   = deniedAudio;
        levelUpAudio = luAudio;
        Level = startingLevel;

        confettiController = FindObjectOfType<ConfettiController>();
    }

    void Update()
    {
        // Game state machine
        switch (gameState)
        {
            default:
                // Should never happen
                break;
            case GameState.Tutorial:
                TutorialHandler();
                break;
            case GameState.Active:
                ActiveHandler();
                break;
            case GameState.Paused:
                PauseHandler();
                break;
        }
    }

    public static GameState GetGameState()
    {
        return gameState;
    }

    public static void SetGameState(GameState state)
    {
        gameState = state;
    }

    void TutorialHandler()
    {
        // TODO
    }

    void ActiveHandler()
    {
        switch(Level) // Only check variables about the level we are currently on
        {
            case 1:
            {
                    // START: Get the state of all phones
                    bool phoneState = true;
                    GameObject[] phones = GameObject.FindGameObjectsWithTag("Phone");
                    foreach (GameObject phone in phones)
                    {
                        phoneState &= phone.GetComponent<PhoneController>().pluggedIn;
                    }
                    phonesPluggedIn = phoneState;
                    // END: Get the state of all phones
            }
            break; // LEVEL 1
            case 2:
            {
                    bool filled = true;
                    GameObject[] printers = GameObject.FindGameObjectsWithTag("Printer");
                    Debug.Log(printers.Length);
                    foreach (GameObject printer in printers)
                    {
                        filled &= printer.GetComponentInChildren<PrinterController>().isFilled;
                    }
                    printersFilled = filled;
            }
            break; // LEVEL 2
            case 3:
            {
                    if (watchedVideos.Contains("rounds") && !monitorsBroken) // Break some monitors after the rounds video has been watched TODO: scatter some trash
                    {
                        MonitorError.breakMonitors(10);
                        monitorsBroken = true;
                    }
            }
            break; // LEVEL 3
        }
    }

    public static void LevelComplete() // Handle Level end things and increment level counter
    {
        GameManager.playerAudioSource.PlayOneShot(levelUpAudio, 0.5f);
        confettiController.throwConfetti();
        Level++;
    }

    void PauseHandler()
    {
        // TODO
    }

}
