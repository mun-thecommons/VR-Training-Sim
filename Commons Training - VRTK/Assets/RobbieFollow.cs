using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobbieFollow : MonoBehaviour
{
    public GameObject thePlayer;
    public float targetDistance;
    public float allowedDistance = 5;
    public GameObject robbie;
    public float speed;
    public RaycastHit shot;
    
    void Update()
    {
        transform.LookAt(thePlayer.transform);
        if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward), out shot))
        {
            targetDistance = shot.distance;
            if(targetDistance >= allowedDistance)
            {
                speed = 0.02f;
                transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, speed);

            }
            else
            {
                speed = 0;
            }
        }
        
    }
}
