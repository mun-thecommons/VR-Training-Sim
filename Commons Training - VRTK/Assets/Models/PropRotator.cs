﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRotator : MonoBehaviour {

	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 10000, 0) * Time.deltaTime);
    }
}
