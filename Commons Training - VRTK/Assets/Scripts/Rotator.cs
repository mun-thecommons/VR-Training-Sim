using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public float speed = 1f;
    private Vector3 rotationVec;

    void Start()
    {
        rotationVec = new Vector3(15, 30, 45);
    }

    // Use this for initialization
    void Update()
    {
        transform.Rotate(rotationVec * Time.deltaTime * speed);
    }
}
