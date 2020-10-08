using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// #Script Description
/// This script controls the paper retreival cart's motion and paper status
/// </summary>
public class CartController : MonoBehaviour
{

    public static bool held = false;
    private bool navCreated = false;

    public float offset; /*!< @brief Offset of the cart from the player  */

    private GameObject hand;
    private GameObject paperBoxes;

    public GameObject navPrefab;
    private GameObject navTarget;

    private void Start()
    {
        paperBoxes = transform.GetChild(12).gameObject; // Paper stack gameobject is attached to the paper cart game object at index 12 
    }

    /*
     * The y value of the cart is usually fixed. This function changes it (e.g. when the player teleports in an elevator)
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

            Vector3 direction = transform.position - hand.transform.position; // The direction the cart should be facing is the vector from the centre of the cart to the player's hand
            direction.y = 0.0f; // No Y component, don't want the cart to rotate up and down
            transform.forward = direction.normalized; // Set the cart's forward vector to the direction vector (normalized)

            // If the direction vector's magnitude is greater than the offset then move the cart in the forward direction
            if (direction.magnitude > offset) { transform.position -= (direction.magnitude - 0.8f) * transform.forward; }
            
            // Let go of the cart if we press the select button
            if (InputHandler.GetButtonDown(InputHandler.ButtonControl.SelectButton)) { held = false; }
        }


    }

    /*
     * If the player's hand is in the handle trigger and the player is pressing the select button
     * then grab the cart.
     * 
     * If the collider is a paper box then enable the paper boxes that are displayed on the cart
     * and destroy the paper box object
     */
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand") && InputHandler.GetButton(InputHandler.ButtonControl.SelectButton))
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
