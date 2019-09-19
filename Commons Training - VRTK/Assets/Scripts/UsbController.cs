using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsbController : MonoBehaviour {
    private static int numOfUSBCollected = 0;
    private GameObject player;
    private bool isUSBTouched= false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update ()
    {
        if (isUSBTouched == false)
        {
            EnableRotation();
        }
        if(numOfUSBCollected >= 1)
        {
            Level.level2USB = true;
        }
    }

    void EnableRotation()
    {
      transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    //USB and USB box collision logic

    void OnTriggerEnter(Collider collider)
    {
        if(MasterController.vestCollected)                           // Lets USB continue to rotate while hand collides with USB   
        {
            if (collider.CompareTag("USBox"))
            {
                numOfUSBCollected++;
                MasterController.ScoreModify(0, 0, 1, true, true);  //played and the player gets a score point  
                Destroy(gameObject);
                RobotController.isInUsbBox = true;
            }
            else if (collider.CompareTag("Hand"))
            {
                RobotController.isTouchingUSB = true;
                isUSBTouched = true;
            }
            else if (!collider.CompareTag("Hand"))
            {
                RobotController.isTouchingUSB = false;
            }
        }
    }
}
