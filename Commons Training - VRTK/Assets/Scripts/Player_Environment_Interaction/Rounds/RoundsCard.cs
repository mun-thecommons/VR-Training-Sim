using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The script controls the position and rotation of the Round Card
/// ##Script Description
/// If the round card is displaced and left unattended, after a preset time the position and rotation of the card will be reset
/// </summary>
public class RoundsCard : MonoBehaviour
{
    // Maximum amount of time card can be left alone
    public float maxTime = 15.0f;

    //how long has the card been unattended
    public float timeUntouched;
    //is it being grabbed
    public bool beingTouched = false;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        timeUntouched = 0.0f;

        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        //the card moves if the position and rotation both change
        bool cardMoved = (transform.position != originalPosition) && (transform.rotation != originalRotation);
        //when the card is unattended, if the time exceeded a maximum amount of time, the card position and rotation are reset
        if (cardMoved && !beingTouched)
        {
            timeUntouched += Time.deltaTime;

            // Reset the card's position if it has been moved and not been touched for enough time
            if (timeUntouched >= maxTime)
            {
                transform.position = originalPosition;
                transform.rotation = originalRotation;

                timeUntouched = 0.0f;
            }

        }

    }

    /*
     * Hand grabbing the card
     * @note: function OnTriggerEnter(Collider other) is used
     * @note: timeUntouched variable is reset
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            beingTouched = true;
            timeUntouched = 0.0f;
        }
    }

    /*
     * Hand releasing the card
     * @note: virtual hands are tagged "Hand"
     * @note: beingTouched varible is reset
     */
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            beingTouched = false;
        }
    }
}
