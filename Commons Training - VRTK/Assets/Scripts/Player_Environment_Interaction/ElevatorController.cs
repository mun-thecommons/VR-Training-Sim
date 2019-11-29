using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ding!");
    }
}
