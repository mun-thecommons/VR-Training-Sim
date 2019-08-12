using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFollow : MonoBehaviour
{
    public GameObject player;
    private float angle;
    public float distance = 2f;

    void LateUpdate()
    {
        angle = (RobotController.isInUsbBox || RobotController.isTouchingUSB) ? (0) : (180);
        transform.position = new Vector3(player.transform.position.x + player.transform.forward.x*distance, player.transform.position.y, player.transform.position.z + player.transform.forward.z*distance);
        transform.RotateAround(player.transform.position, player.transform.up, angle);
        transform.LookAt(player.transform);
    }
}
