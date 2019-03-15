using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light3 : MonoBehaviour {

    void Update()
    {
        if (RoundsCard.isRound3Done)
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        else
            gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
