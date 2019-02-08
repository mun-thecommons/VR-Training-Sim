using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light2 : MonoBehaviour {

    void Update()
    {
        if (TouchDetection.station2)
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        else
            gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
