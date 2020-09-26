using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Teleport a player in an elevator to the secondary elevator
/// 
/// ##Script Description
/// Control a pair of elevators. Script references other elevator to 
/// perform actions involving both elevators. The script only works 
/// with elevators that move the player to one of two levels
/// </summary>
public class ElevatorController : MonoBehaviour
{ 
    public bool inElevator; /*!< @brief Make sure that the player is in the elevator  */
    public bool isEnabled;

    public float yOffset; /*!< @brief offset to the secondary elevator  */

    public AudioClip ding;

    // Variables referencing the primary elevator
    private Animator doorAnim;
    private AudioSource elevatorAudio;

    // Variables referencing the secondary elevator
    public GameObject secondaryElevator;
    private Animator secondaryAnim;
    private AudioSource secondaryAudio;

    private void Start()
    {
        elevatorAudio  = GetComponent<AudioSource>();
        secondaryAudio = secondaryElevator.GetComponent<AudioSource>();

        doorAnim      = GetComponent<Animator>();
        secondaryAnim = secondaryElevator.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            inElevator = !inElevator; // Toggle the state of inElevator

            if (isEnabled && inElevator) // Player is in the elevator and the elevator is enabled
            {
                // If the player is entering the elevator,
                // start the elevator coroutine
                StartCoroutine(ElevatorCoroutine());
            }
            else if (isEnabled) // Player is leaving the elevator and the elevator is enabled
            {
                // Play door close animation
                doorAnim.SetTrigger("CloseDoor");
                doorAnim.SetBool("IsClosed", true);
            }
        }
    }


    /*****************************************************************************************************
     * Handle the elevator's "movement"
     * 
     * This script needs to be a coroutine because it waits for some time to pass 
     * for the elevator to move to the next floor
     * 
    ***********************************************************/
    IEnumerator ElevatorCoroutine()
    {
        yield return new WaitForSeconds(2); // Give the player a few seconds to get into the elevator

        // Close the elevator doors
        doorAnim.SetTrigger("CloseDoor");
        doorAnim.SetBool("IsClosed", false);

        elevatorAudio.PlayDelayed(1.0f); // Play elevator music
        
        yield return new WaitForSeconds(Random.Range(5.0f, 10.0f)); // Wait some time for the elevator to "move" 

        elevatorAudio.Stop();

        // Teleport the player down to the secondary elevator
        InputHandler.TeleportPlayer(InputHandler.position + new Vector3(0, yOffset, 0), InputHandler.rotation);

        if (CartController.held) { CartController.ChangeYPos(yOffset); } // Teleport the cart too if the player is carrying it

        // Open the doors on the secondary elevator
        secondaryAudio.PlayOneShot(ding);
        secondaryAnim.SetTrigger("OpenDoor");
        secondaryAnim.SetBool("IsClosed", true);
    }
}
