using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
/// <summary>
/// Holds the logic involved with the Player's Level progress
/// 
/// ##Detailed
/// Has a set of tasks assigned to each of 3 levels. These tasks are stored in Bool variables and as they are completed
/// the Mainmenu UI will update. 
/// 
/// @note This script may change based upon the direction that the VR Project goes in terms of rewarding the Player for achievements
/// </summary>
public class Level : MonoBehaviour
{
    //Player and Start Position
    public GameObject player;
    private Vector3 startPosition;

    //Integer 
    public static int level = 1;
    public TextMeshProUGUI mainFrameText;
    
    //Boolean For Tasks in Levels
    //Level 1 
    public static bool level1Vest = false;                            /*!< @briefCollect the RedVest */
    public static bool level1USB = false;                            /*!< @brief Collect a Singular USB*/
    public static bool level1Printer = false;                       /*!< @brief Fill the printers with paper */

    //Level 2
    public static bool level2Round = false;                            /*!< @brief Complete the rounds */
    public static bool level2Client = false;                          /*!< @brief Answer a Multiple Choice Client */
    public static bool level2Trash = false;                          /*!< @brief Collect a piece of trash around the Commons */
    public static bool level2Cash = false;                          /*!< @brief Help a client at the Cash box*/

    //Level 3
    public static bool level3Monitor = false;                        /*!< @brief Fix a broken Monitor*/
    public static bool level3ClientDesk = false;                    /*!< @brief Help a Client at the Desk*/
    public static bool level3ClientLab = false;                    /*!< @brief Help a client in the Computer lab*/

    //Level 4

    //Timer 
    public float timer = 30f;
    public float resetTimer = 30f;

    //Count Down Timer Canvas
    public GameObject countDown;
    public GameObject levelCompleted;
    private TextMeshProUGUI countDownText;
    private TextMeshProUGUI levelCompletedText;

    private GameObject robbie;                                     /*!< @brief Used to Interact with Robbie */

    /************************************************
     * Initializes required variables for the functions below
     * *********************************************/
    void Start()
    {
        startPosition = player.transform.position;
        countDownText = countDown.GetComponent<TextMeshProUGUI>();
        levelCompletedText = levelCompleted.GetComponent<TextMeshProUGUI>();
        levelCompletedText.enabled = false;
        robbie = GameObject.FindGameObjectWithTag("Robbie");
    }

    /************************************************
     * Ensures the tutorial is complete before continuing with level progression
     * 
     * ##Detailed
     * Checks to ensure that the tutorial was finished by the user. Once the tutorial has been finished it will continue to 
     * monitor the players level progression and reward them for completing levels based on functions below.
     * *********************************************/
    void Update()
    {

    }

    /************************************************
     * Displays the Level complete message
     * 
     * @note This may be tinkered with later to give more of a sense of achievement and instill the Positive reinforcement
     * that we want the Players to feel.
     * 
     * *********************************************/
    IEnumerator DisplayLevelCompleted(float time)
    {
        levelCompletedText.enabled = true;
        levelCompletedText.text = "Level : " + level + " Complete";
        yield return new WaitForSeconds(time);
        levelCompletedText.enabled = false;
    }

    /************************************************
     * Informs player to go to desk starts a timer for it
     * 
     * *********************************************/
    private void WalkToDesk()
    {
        countDownText.text = "Go to CS Desk: " + timer.ToString("F2");
        if (timer <= 0f && !MasterController.vestCollected && !MasterController.inMenu)
        {
            countDownText.text = "Out of time";
            if (timer <= -3f)
            {
                player.transform.position = startPosition;
                timer = resetTimer;
            }
        }
        else if (MasterController.vestCollected && timer >= -3f)
        {
            countDownText.enabled = false;
        }

    }
    /************************************************
     * Verifies all tasks are complete to advance in level
     * 
     * ##Detailed
     * Once it is verified that all tasks are complete to advance to next level, the DisplayLevelCompleted Function will run, congratulating the player
     * and giving some Positive reinforcement for their effort.
     * 
     * *********************************************/
    private void CheckLevel()
    {
        SetMainframeText();
        if (level1Vest && level1Printer && level1USB && level == 1)
        {
            StartCoroutine(DisplayLevelCompleted(5));
            level += 1;
        }
        if (level2Client && level2Round && level2Trash && level2Cash && level == 2)
        {
            StartCoroutine(DisplayLevelCompleted(5));
            level += 1;
        }
        if (level3Monitor && level3ClientDesk && level3ClientLab && level == 3)
        {
            StartCoroutine(DisplayLevelCompleted(5));
            level += 1;
        }
    }
    /************************************************
     * Used within the MainMenu UI to display current Tasks to complete
     * 
     * ##Detailed
     * Task list of what needs to be done to complete the current level. Once all tasks are complete for the level a new list of tasks will
     * appear in its place for the new level.
     *
     * *********************************************/
    private void SetMainframeText()
    {
        if(level == 1)
        {
            mainFrameText.text = "Level: " + Level.level + "\n Get vest: " + (level1Vest ? "Complete" : "Incomplete")
                + "\n Fill printers: " + (level1Printer ? "Complete" : "Incomplete")
                + "\n Return USB: " + (level1USB ? "Complete" : "Incomplete");
        }
        else if(level == 2)
        {
            mainFrameText.text = "Level: " + Level.level + "\n Help a client in the lab: " + (level2Client ? "Complete" : "Incomplete")
                   + "\n Complete rounds: " + (level2Round ? "Complete" : "Incomplete")
                   + "\n Pick up garbage: " + (level2Trash ? "Complete" : "Incomplete")
                   + "\n Help a Client with their Campus Card: " + (level2Cash ? "Complete" : "Incomplete");
        }
        else if (level == 3)
        {
            mainFrameText.text = "Level: " + Level.level + "\n Fix a monitor: " + (level3Monitor ? "Complete" : "Incomplete")
                   + "\n Help a client at the desk: " + (level3ClientDesk ? "Complete" : "Incomplete")
                   + "\n Help more clients in the lab: " + (level3ClientLab ? "Complete" : "Incomplete");
        }
    }
    
}
