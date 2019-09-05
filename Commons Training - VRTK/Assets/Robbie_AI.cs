using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robbie_AI : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 3.0f;
    float moveSpeed;


    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) >= 3.0f)
        {
            moveSpeed = 3.0f;
        }
        else
        {
            moveSpeed = 0.0f;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position),
                rotationSpeed*Time.deltaTime);

        while (Vector3.Distance(transform.position, player.position) >= 1.0f)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

    }
}
