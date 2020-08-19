using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour
{

    public static bool held = false;

    private bool navCreated = false;

    public float offset;

    private GameObject hand;
    private GameObject paperBoxes;

    public GameObject navPrefab;

    private GameObject navTarget;

    private void Start()
    {
        paperBoxes = transform.GetChild(12).gameObject;
    }

    /*
     * Increment the y position of the cart by deltaY
     */
    public static void ChangeYPos(float deltaYPos)
    {
        GameObject cart = GameObject.FindGameObjectWithTag("Cart");

        cart.transform.position = new Vector3(cart.transform.position.x, cart.transform.position.y + deltaYPos, cart.transform.position.z);
    }

    void Update()
    {
        if (held)
        {

            if (!navCreated)
            {
                navTarget = Instantiate(navPrefab, new Vector3(18.5f, 0.0f, 19f), navPrefab.transform.rotation);
                navCreated = true;
            }
            else if (navTarget == null)
            {
                Debug.Log("Destroyed!");
            }

            Vector3 direction = transform.position - hand.transform.position;
            direction.y = 0.0f;
            transform.forward = direction.normalized;

            if (direction.magnitude > offset)
            {
                transform.position -= (direction.magnitude - 0.8f) * transform.forward;
            }

            // transform.position = new Vector3(hand.transform.position.x, transform.position.y, hand.transform.position.z + zOffset);

            if (InputHandler.selectButton.isDown)
            {
                held = false;
            }
        }


    }

    /*
     */
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand") && InputHandler.selectButton.state)
        {
            hand = other.gameObject;
            held = true;
        }
        else if (other.CompareTag("Paper Box") && paperBoxes.activeSelf == false)
        {
            Destroy(other.gameObject);
            paperBoxes.SetActive(true);
        }
    }
}
