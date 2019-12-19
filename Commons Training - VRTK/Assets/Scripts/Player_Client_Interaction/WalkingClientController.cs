using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingClientController : MonoBehaviour
{
    private float yPos = 0.08f;
    static public List<Vector3> walkDestPos = new List<Vector3>();
    private int destPoint = 0;
    private NavMeshAgent agent;

    void Start()
    {
        walkDestPos.Add(new Vector3(4.21f, yPos, -18.21f));
        walkDestPos.Add(new Vector3(1.16f, yPos, -15.5f));
        walkDestPos.Add(new Vector3(10.13f, yPos, -10.86f));
        walkDestPos.Add(new Vector3(7.14f, yPos, -9.22f));
        walkDestPos.Add(new Vector3(-16.67f, yPos, -11.18f));
        walkDestPos.Add(new Vector3(-12.24f, yPos, -16.75f));
        walkDestPos.Add(new Vector3(-4.79f, yPos, -20.68f));
        walkDestPos.Add(new Vector3(6.54f, yPos, -37.66f));
        walkDestPos.Add(new Vector3(24.57f, yPos, -32.96f));
        walkDestPos.Add(new Vector3(22.42f, yPos, -29.17f));
        walkDestPos.Add(new Vector3(18.45f, yPos, -23.68f));
        walkDestPos.Add(new Vector3(8.14f, yPos, -25.9f));
        walkDestPos.Add(new Vector3(-22f, yPos, -40f));
        walkDestPos.Add(new Vector3(24.9f, yPos, 27.6f));
        walkDestPos.Add(new Vector3(3.7f, yPos, 34.2f));
        walkDestPos.Add(new Vector3(21.2f, yPos, -29.8f));
        walkDestPos.Add(new Vector3(-52.1f, yPos, -24.8f));
        walkDestPos.Add(new Vector3(-50.3f, yPos, -41.2f));
        walkDestPos.Add(new Vector3(-42.8f, yPos, -30.7f));

        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        destPoint = Random.Range(0, walkDestPos.Count-1);
    }

    void GotoNextPoint()
    {
        if (walkDestPos.Count == 0)
            return;

        agent.destination = walkDestPos[destPoint];
        destPoint = (destPoint + 1) % (walkDestPos.Count);
    }

    void Update()
    {
        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }
}
