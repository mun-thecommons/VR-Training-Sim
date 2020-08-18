using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Adds a varying glow to a light
/// 
/// ##Detailed
/// This script picks up the light component attached to a gameobject and with each called update it will change the intensity of the
/// light, creating a glow effect.
/// 
/// </summary>
public class Glow : MonoBehaviour
{
    private Light glow;             /*!< @brief Upon start gets the Light component and stores it here.*/
    private int multiplier = 3;     /*!< @brief Simple multiplier used within Update function.*/

    // Start is called before the first frame update
    void Start()
    {
        glow = GetComponent<Light>();
    }

    /********************************************
     * The glowing effect will increase or decrease based on the state of the multiplier.
     * 
     * ****************************************/
    void Update()
    {
        if ((glow.intensity >= 1 && multiplier > 0) || (glow.intensity <= 0.01 && multiplier < 0))
        {
            multiplier *= -1;
        }
        glow.intensity += multiplier*Time.deltaTime;
    }
}
