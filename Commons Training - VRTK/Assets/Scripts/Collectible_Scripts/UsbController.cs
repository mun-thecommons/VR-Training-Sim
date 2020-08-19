using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Houses all Logic pertaining to the USB interactions
/// 
/// ##Detailed
/// This script is what connects the USB and the players actions together to generate scoring, as well as
/// progress the Level system. Because of this the script will use variables from both the Level and MasterController scripts.
/// 
/// Interactions with the USB will only commence once the player has obtained the RedVest from the main desk area.
/// 
/// </summary>
public class UsbController : MonoBehaviour {

    private static int numOfUSBCollected = 0;   /*!< @brief Stores how many USBs have been collected */
    private GameObject player;      /*!< @brief Stores the Player object */
    private bool isUSBTouched= false;   /*!< @brief Bool variable used for Collider Logic */

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /************
     * Checks how many USB's have been collected
     * 
     * If the USB is not currently being touched then rotation is to remain enabled
     * 
     * If the number of USBs collected is equal to or greater than 1, than the Level 1 task of 
     * collecting USBs will be set to True within the Level script
     * 
     * @see Level
     * **********/
    void Update ()
    {
        if (isUSBTouched == false)
        {
            EnableRotation();
        }
        if(numOfUSBCollected >= 1)
        {
            Level.level1USB = true;
        }
    }
    /**********************
     * @brief Enables rotation of the USB gameobject.
     * 
     * Mimics the same function of the Rotator script but it does not allow for any change of rotational speed. 
     * Since this function is built into the script it will only interact with the USB
     * 
     * *****************/
    void EnableRotation()
    {
      transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

   /**********************************************
    * USB and Lost USB box collider Logic.
    * 
    * ##Detailed
    * If the player is wearing their RedVest then the logic will execute based on what is colliding with the USB.
    * If the player is not wearing their RedVest nothing will happen upon collision.
    * 
    * If the collision is detectewd with the Hand Collider of the player then it will inform the RobtoController of the collision
    * It will also change the Bool variable "IsUSBTouched" to true.
    * 
    * Once the USB is brought back to the USB box and their collision is detected the tally of USB's collected increases, 
    * the scoremodifier increases, the USB is destroyed as to replicate it being placed in the box, and lastly the 
    * RobotController that the UISB is in the Box.
    * 
    * @note For details on the ScoreModify Function see MasterController script.
    * @see MasterController; RobotController
    * **********************************************/
    void OnTriggerEnter(Collider collider)
    {
        if(MasterController.vestCollected)                           // Lets USB continue to rotate while hand collides with USB   
        {
            if (collider.CompareTag("USBox"))
            {
                numOfUSBCollected++;
                MasterController.ScoreModify(0, 0, 1, true, true);  //played and the player gets a score point  
                Destroy(gameObject);
                RobotController.isInUsbBox = true;
            }
            else if (collider.CompareTag("Hand"))
            {
                RobotController.isTouchingUSB = true;
                isUSBTouched = true;
            }
            else if (!collider.CompareTag("Hand"))
            {
                RobotController.isTouchingUSB = false;
            }
        }
    }
}
