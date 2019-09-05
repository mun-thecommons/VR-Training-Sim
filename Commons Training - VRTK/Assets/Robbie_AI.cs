using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robbie_AI : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 3.0f;
    public float moveSpeed = 3.0f;


    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position),
                rotationSpeed*Time.deltaTime);
        while (Vector3.Distance(transform.position, player.position) >= 3.0f)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
