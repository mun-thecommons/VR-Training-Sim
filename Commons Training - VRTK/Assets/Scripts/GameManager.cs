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
    public static bool hasVest = false;
    public static bool monitorsOn = false;
    public static bool safeOpen = false;

    public GameObject libraryRoof;

    public AudioSource playerAudio;
    public static AudioSource playerAudioSource; // Map audio source to a static variable to make it easier to play audio focused on player

    private GameObject robbie;
    private RobbieController robbieController;

    private GameObject player;

    public static GameState gameState;
    public static int Level = 1;

    // Level one state variables
    public static bool phonesPluggedIn = false;



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
                    // END: Get the stae of all phones
            }
            break; // LEVEL 1

        }
    }

    void PauseHandler()
    {
        // TODO
    }

}
