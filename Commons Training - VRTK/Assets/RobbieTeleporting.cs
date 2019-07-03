using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobbieTeleporting : MonoBehaviour
{
    public GameObject player;
    public float maxPlayerRobbieDistance = 6f; 

    private float playerRobbieDistance;
    private RaycastHit shot;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        playerRobbieDistance = Vector3.Distance(transform.position, player.transform.position);
        if(playerRobbieDistance >= maxPlayerRobbieDistance)
        {
            transform.position = new Vector3(player.transform.position.x + player.transform.forward.x*5, transform.position.y, player.transform.position.z + player.transform.forward.z * 5);
            RobbieTeleport();
        }
        transform.LookAt(player.transform);
    }

    void RobbieTeleport()
    {
        if (rb.SweepTest(transform.forward, out shot, 0))
        {
            Debug.Log("rotating");
            transform.RotateAround(player.transform.position, player.transform.up, 1);
            RobbieTeleport();
        }
        return;
    }
}
