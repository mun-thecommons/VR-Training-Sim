using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobbieFollow : MonoBehaviour
{
    public GameObject player;
    public float targetDistance;
    public float allowedDistance = 5;
   
    private float speed = 0.02f;
    public RaycastHit shot;
    
    void Update()
    {
        transform.LookAt(player.transform);
        if(Vector3.Distance(transform.position, player.transform.position) > allowedDistance)    
       {
            PathFind();
            transform.position += transform.forward * Time.deltaTime;
                    //  transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
        }
    }
    void PathFind()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot, 5))
        {
            transform.Rotate(0, 1, 0);
            PathFind();
        }
        return;
    }
}
