using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{

    public Transform center;
    public float degreesPerSecond = -65.0f;

    private Vector3 v;

    void Start()
    {
        v = transform.position - center.position;
    }

    void Update()
    {
        transform.Rotate(new Vector3(-20,-62, 0) * Time.deltaTime);
        v = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.up) * v;
        transform.position = center.position + v;
    }
}

