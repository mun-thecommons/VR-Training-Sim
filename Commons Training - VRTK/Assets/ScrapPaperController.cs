using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapPaperController : MonoBehaviour
{
    private GameObject player;
    private bool isUSBTouched = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isUSBTouched == false)
        {
            EnableRotation();
        }
    }

    void EnableRotation()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }


    void OnTriggerEnter(Collider collider)
    {
        if (MasterController.vestCollected)                           
        {
           
            if (collider.CompareTag("Hand"))
            {
                RobotController.isTouchingUSB = true;
                isUSBTouched = true;

                MasterController.scrapPaper++;

            }
            if (!collider.CompareTag("Hand"))
            {
                RobotController.isTouchingUSB = false;
            }
        }
        
    }
}
