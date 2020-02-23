using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigatonTarget : MonoBehaviour
{

    public GameObject lineObject;
    private GameObject player;

    private LineRenderer navLine;

    private Vector3[] positions = new Vector3[2];

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navLine = lineObject.GetComponent<LineRenderer>();
        positions[0] = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        positions[0] -= transform.position;
        positions[1] = Vector3.zero;
        navLine.SetPositions(positions);
        navLine.startWidth = 0.5f;
        navLine.endWidth = 0.5f;
    }

    private void Update()
    {
        positions[0] = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        positions[0] -= transform.position;
        positions[1] = Vector3.zero;
        navLine.SetPositions(positions);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
