using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script controlling the motion and logic of the stapler
/// 
/// ##Detailed
/// Changes the vector location of the stapler object each frame update and also determines what happens 
/// during a collision with another object based upon the object
/// </summary>
public class StaplerShoot : MonoBehaviour
{
    public GameObject whiteSmokePrefab; /*!< @brief Used for when a monitor/printer is shot */
    private GameObject whiteSmoke;      /*!< @brief Used for when a monitor/printer is shot */
    public float timeDestroy = 2.0f;    /*!< @brief Time until stapler disappears after being shot*/
    public float speed = 2f;            /*!< @brief speed of travel for the stapler */
    private Vector3 forward;            /*!< @brief Directional vector used for stapler travel */

    private void Start()
    {
        Destroy(gameObject, timeDestroy);
        forward = transform.forward;
    }
    /************************************
     * Moves the stapler forward during each update
     * 
     * ##Detailed
     * During each frame update the vector location of the shot stapler will move forward an amount determined based on the speed 
     * variable set.
     * *********************************/
    void Update()
    {
        transform.position += forward * Time.deltaTime * speed;
    }

    /*************************************
     * Detects what the stapler collides with
     * 
     * ##Detailed
     * During a collision the scripts checks the tag of the Gameobject collided with. If the collision is with
     * a Monitor or Printer they will be destroyed and a White Smoke prefab will instantiate in its place. 
     * 
     * If it is a client then it will decrement a scorepoint.
     * If it is an alien then it will trigger the "Alien Shot" animation.
     * **********************************/
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monitor") || other.CompareTag("Printer"))
        {
            MasterController.ScoreModify(-1,0,0,false,false);
            whiteSmoke = Instantiate(whiteSmokePrefab, other.transform.position, other.transform.rotation) as GameObject;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Client"))
        {
            MasterController.ScoreModify(-1,0,0,false,true);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Alien"))
        {
            AlienManager.ShootAlien(transform.position);
        }
    }
}
