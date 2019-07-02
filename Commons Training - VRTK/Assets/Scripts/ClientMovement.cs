using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMovement : MonoBehaviour
{
    private float speed = 0.8f;
    public RaycastHit shot;
    private Vector3 destinationPosition;

    // Start is called before the first frame update
    void Start()
    {
        destinationPosition = new Vector3(-26.8f, 1.26f, -31.4f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(destinationPosition);
        if (Vector3.Distance(transform.position, destinationPosition) > 0)
        {
            PathFind();
            transform.position += transform.forward * Time.deltaTime;
        }
    }

    void PathFind()
    {
        if (GetComponent<Rigidbody>().SweepTest(transform.TransformDirection(Vector3.forward), out shot, 2))
        {
            transform.Rotate(0, 1, 0);
            PathFind();
        }
        return;
    }
}
