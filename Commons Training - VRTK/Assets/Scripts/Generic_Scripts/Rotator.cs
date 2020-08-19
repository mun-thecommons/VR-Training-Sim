using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Adds a rotation to attached Gameobject.
/// 
/// ##Detailed
/// Using Vector3 coordinates, the gameobject is rotateted based on the speed and time passed. 
/// If the speed is increased or decreased the rotation will be affected. 
/// 
/// @note Be careful when placing script on Gameobjects with collider boxes as it may push or block other Gameobjects. In these instances
/// be sure to have IsTrigger activated so the collider doesn't cause issues
/// 
/// </summary>
public class Rotator : MonoBehaviour {

    public float speed = 1f;
    private Vector3 rotationVec;    /*!< @brief Gives the (x,y,z) coordinates of the vector*/

    // Use this for initialization
    void Start()
    {
        rotationVec = new Vector3(15, 30, 45);
    }

    //Gameobject will rotate based on the change in time multiplied by the set speed variable.
    void Update()
    {
        transform.Rotate(rotationVec * Time.deltaTime * speed);
    }
}
