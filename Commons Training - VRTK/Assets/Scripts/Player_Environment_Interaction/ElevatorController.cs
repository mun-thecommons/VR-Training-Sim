using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{ 
    
    // Ensure the player is only teleported if they are entering the elevator, not leaving
    public bool inElevator;
    public bool isEnabled;

    public float yOffset;

    private Animator doorAnim;

    public GameObject secondaryElevator;
    private Animator secondaryAnim;

    private AudioSource elevatorAudio;
    private AudioSource secondaryAudio;

    public AudioClip ding;


    private void Start()
    {
        elevatorAudio = GetComponent<AudioSource>();
        secondaryAudio = secondaryElevator.GetComponent<AudioSource>();

        doorAnim = GetComponent<Animator>();

        secondaryAnim = secondaryElevator.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Toggle the state of inElevator
            inElevator = !inElevator;
            if (isEnabled && inElevator)
            {
                // If the player is entering the elevator,
                // start the elevator coroutine
                StartCoroutine(ElevatorCoroutine());
            }
            else if (isEnabled)
            {
                // if the player is leaving the elevator,
                // close the doors behind them
                doorAnim.SetTrigger("CloseDoor");
                doorAnim.SetBool("IsClosed", true);
            }
        }
    }

    // Elevator coroutine. Needed to use 
    // WaitForSeconds()
    IEnumerator ElevatorCoroutine()
    {
        // Give the player a few seconds to get into the elevator
        yield return new WaitForSeconds(2);

        // Close the elevator doors
        doorAnim.SetTrigger("CloseDoor");
        doorAnim.SetBool("IsClosed", false);

        elevatorAudio.PlayDelayed(1.0f);

        // Wait some time for the elevator to "move"
        yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));

        elevatorAudio.Stop();

        // Teleport the player down to the secondary elevator
        InputHandler.TeleportPlayer(InputHandler.position + new Vector3(0, yOffset, 0), InputHandler.rotation);
        if (CartController.held)
        {
            CartController.ChangeYPos(yOffset);
        }
        // Open the doors on the secondary elevator
        secondaryAudio.PlayOneShot(ding);
        secondaryAnim.SetTrigger("OpenDoor");
        secondaryAnim.SetBool("IsClosed", true);
    }
}
