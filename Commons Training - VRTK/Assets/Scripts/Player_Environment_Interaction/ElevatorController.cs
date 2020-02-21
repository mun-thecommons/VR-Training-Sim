using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{ 
    private Animator doorAnim;
    public bool isEnabled;

    public GameObject secondaryElevator;
    private Animator secondaryAnim;

    // Ensure the player is only teleported if they are entering the elevator, not leaving
    private bool inElevator = false;

    private void Start()
    {
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

        // Wait some time for the elevator to "move"
        yield return new WaitForSeconds(3);

        // Teleport the player down to the secondary elevator
        InputHandler.TeleportPlayer(InputHandler.position + new Vector3(0, -10, 0), InputHandler.rotation);
        if (CartController.held)
        {
            CartController.ChangeYPos(-10);
        }
        // Open the doors on the secondary elevator
        secondaryAnim.SetTrigger("OpenDoor");
        secondaryAnim.SetBool("IsClosed", true);
    }
}
