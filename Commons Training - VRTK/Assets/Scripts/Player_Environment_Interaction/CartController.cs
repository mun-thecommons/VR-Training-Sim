using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour
{

    public bool held = false;

    public float offset;

    private GameObject hand;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (held)
        {
            Vector3 direction = transform.position - hand.transform.position;
            direction.y = 0.0f;
            transform.forward = direction.normalized;

            if (InputHandler.selectButton.isDown)
            {
                held = false;
            }

            if (direction.magnitude > offset)
            {
                transform.position -= (direction.magnitude - 0.8f) * transform.forward;
            }

            // transform.position = new Vector3(hand.transform.position.x, transform.position.y, hand.transform.position.z + zOffset);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand") && InputHandler.selectButton.isDown)
        {
            hand = other.gameObject;
            held = true;
        }
    }
}
