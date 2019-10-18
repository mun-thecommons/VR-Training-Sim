using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienController : MonoBehaviour
{
    private float yPos = 1.0f;
    static public List<Vector3> walkDestPos = new List<Vector3>();
    private int destPoint = 0;
    private NavMeshAgent agent;

    void Start()
    {
        walkDestPos.Add(new Vector3(0.0f, yPos, -3.6f));
        walkDestPos.Add(new Vector3(9.7f, yPos, -12.0f));
        walkDestPos.Add(new Vector3(3.9f, yPos, -12.0f));
        walkDestPos.Add(new Vector3(3.9f, yPos, -29.3f));
        walkDestPos.Add(new Vector3(29.1f, yPos, -20.6f));
        walkDestPos.Add(new Vector3(-12.9f, yPos, -23.3f));
        walkDestPos.Add(new Vector3(-27.0f, yPos, -23.3f));
        walkDestPos.Add(new Vector3(-36.7f, yPos, -8.1f));
        walkDestPos.Add(new Vector3(-17.6f, yPos, 18.2f));
        walkDestPos.Add(new Vector3(1.4f, yPos, 18.2f));
        walkDestPos.Add(new Vector3(5.5f, yPos, 7.0f));

        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        destPoint = Random.Range(0, walkDestPos.Count);
    }

    void GotoNextPoint()
    {
        if (walkDestPos.Count == 0)
            return;
        agent.destination = walkDestPos[destPoint];
        destPoint = (destPoint + 1) % walkDestPos.Count;
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }
}
