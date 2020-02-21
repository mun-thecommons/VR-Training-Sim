using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryElevatorController : MonoBehaviour
{
    private Animator doorAnim;
    public bool isEnabled;

    public GameObject primaryElevator;
    private Animator primaryAnim;

    private bool inElevator = true;

    private void Start()
    {
        doorAnim = GetComponent<Animator>();

        primaryAnim = primaryElevator.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inElevator = !inElevator;
            if (isEnabled && inElevator)
            {
                StartCoroutine(ElevatorCoroutine());
            }
            else if (isEnabled)
            {
                doorAnim.SetTrigger("CloseDoor");
                doorAnim.SetBool("IsClosed", true);
            }
        }
    }

    IEnumerator ElevatorCoroutine()
    {
        yield return new WaitForSeconds(2);

        doorAnim.SetTrigger("CloseDoor");
        doorAnim.SetBool("IsClosed", false);

        yield return new WaitForSeconds(3);

        InputHandler.TeleportPlayer(InputHandler.position + new Vector3(0, 10, 0), InputHandler.rotation);

        if (CartController.held)
        {
            CartController.ChangeYPos(10);
        }

        primaryAnim.SetTrigger("OpenDoor");
        primaryAnim.SetBool("IsClosed", true);
    }
}
