using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapPaperController : MonoBehaviour
{
    private GameObject player;
    private bool isUSBTouched = false;
    private OVRGrabbable grabbableScript;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        grabbableScript = gameObject.GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        grabbableScript.enabled = true;
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

                CollectibleManager.CollectScrapPaper(transform.position);
                Destroy(gameObject);

            }
            if (!collider.CompareTag("Hand"))
            {
                RobotController.isTouchingUSB = false;
            }
        }
        
    }
}
