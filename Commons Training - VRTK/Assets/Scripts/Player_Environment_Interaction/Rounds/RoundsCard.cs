using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundsCard : MonoBehaviour
{
    // Time in seconds that the rounds card
    // can be left alone before resetting.
    public float maxTime = 15.0f;

    private float timeUntouched;
    private bool beingTouched = false;

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
        bool cardMoved = (transform.position != originalPosition) && (transform.rotation != originalRotation);

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            beingTouched = true;
            timeUntouched = 0.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            beingTouched = false;
        }
    }
}
