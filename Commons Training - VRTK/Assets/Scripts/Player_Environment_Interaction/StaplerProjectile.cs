using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ##Script Description
/// This script controls the collection and shooting of staplers
/// </summary>
public class StaplerProjectile : MonoBehaviour
{
    private float timer;
    private float staplerInterval;
    public static int staplers = 100;

    public GameObject staplerPrefab;
    public GameObject rightHand;


    // Start is called before the first frame update
    void Start()
    {
        timer = 1.0f;
        staplerInterval = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (InputHandler.GetButton(InputHandler.ButtonControl.StaplerButton) && timer >= staplerInterval)
        {
            ShootStapler();
            timer = 0.0f;
        }
    }

    /*
     * If there are staplers available in the inventory, the player can shoot stapler from his/her right hand
     */
    void ShootStapler()
    {
        if (staplers > 0)
        {
            Instantiate(staplerPrefab, rightHand.transform.position, rightHand.transform.rotation);
            staplers--;
        }
    }
    /*
     * The player can collect steapler inventory by triggering the stapler game objects
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StaplerCollectible"))
        {
            CollectibleManager.CollectStapler(other.gameObject.transform.position);
            Destroy(other.gameObject);
        }
    }
}
