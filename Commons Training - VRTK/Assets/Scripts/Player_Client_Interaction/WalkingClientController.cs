using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Enables Clients to walk around with no questions
/// 
/// ##Detailed
/// A list of vector positions have been designated as destination points and with the use of the NavMeshAgent the clients will move to said locations. 
/// The script chooses a new destination upon arriving the original, and there are walking animations attached to the clients so the movement looks more realistic.
/// </summary>
public class WalkingClientController : MonoBehaviour
{
    private float yPos = 0.08f;
    static public List<Vector3> walkDestPos = new List<Vector3>();
    private int destPoint = 0;
    private NavMeshAgent agent; /*!< @brief Used to interact with the attached GameObjects NavMeshAgent*/

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

        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        destPoint = Random.Range(0, walkDestPos.Count);
    }

    /***************************************
     * Function which controls the next point to go too
     * ##Detailed
     *  After a destination is reached a random point will be chosen afterward
     * 
     * ***********************************/
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
