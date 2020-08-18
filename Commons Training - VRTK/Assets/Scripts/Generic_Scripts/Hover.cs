using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Allows objects to have a hover effect
/// 
/// ##Detailed 
/// This script will give an object a hover effect based on two parameters. 
/// The amplitude will decide how high the object will hover, and the frequency how often it will bob up and down. 
/// 
/// Initial values has been set, but a User may change these to have a more noticable change of motion. 
/// This is done within Unity.
/// </summary>
public class Hover : MonoBehaviour
{

    // Basic Input.
    public float amplitude = 0.5f;  /*!< @brief Amplitude of the Gameobjects Sin function */
    public float frequency = 1f;    /*!< @brief Frequency of the Gameobjects Sin function */

    // Position Storage Variables
    float posOffset;    /*!< @brief Position storage variable */
    float tempPos;      /*!< @brief Position storage variable */


    //Start is called before the first frame update
    void Start()
    {
        
          posOffset = transform.position.y;     // Store the starting position & rotation of the object
    }

    // Update is called once per frame
    void LateUpdate()
    {
       
        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = new Vector3(transform.position.x, tempPos, transform.position.z);
    }
}





