using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour {
   
    static private float cashBoxTimerReset = 15f;
    public GameObject cashBoxClient;
    public GameObject mcClient;
    static private List<Vector3> spawnPositions = new List<Vector3>();
    private Vector3 spawnPosition;
    //static public GameObject[] destinationPositions;
    static public List<GameObject> destinationPositions = new List<GameObject>();
    private GameObject client;
    static private float timer = 2.5f;
    static private float resetTimer = 5f;
    private int randInd;
    private float yPos = 0.08f;
    static public int clientsSpawned = 0;
    private Transform goal;
    UnityEngine.AI.NavMeshAgent agent;

    Animator animator;

    private void Start()
    {
        

        spawnPositions.Add(new Vector3(-30.28f, yPos, -35.9f));
        spawnPositions.Add(new Vector3(-27.957f, yPos, -35.9f));
        spawnPositions.Add(new Vector3(-27.73f, yPos, -38.6f));
        spawnPositions.Add(new Vector3(-28.957f, yPos, -35.9f));
        destinationPositions.AddRange(GameObject.FindGameObjectsWithTag("Destination"));
        Debug.Log("number of destination points is "+destinationPositions.Count);



    }

    void Update ()
    {
        timer -= Time.deltaTime;
        SpawnClient();
	}

    void SpawnClient()
    {
        //if (timer <= 0 && clientsSpawned == 0 && spawnPositions.Count > 0)
            if (timer <= 0 && clientsSpawned <= spawnPositions.Count && spawnPositions.Count > 0)
        {
            randInd = Random.Range(0, spawnPositions.Count);
            spawnPosition = spawnPositions[randInd];
            goal = destinationPositions[randInd].transform;
            spawnPositions.RemoveAt(randInd);
            destinationPositions.RemoveAt(randInd);
            
            if (false)
            {
                client = Instantiate(cashBoxClient, spawnPosition, cashBoxClient.transform.rotation) as GameObject;
            }
            else
            {
                client = Instantiate(mcClient, spawnPosition, mcClient.transform.rotation) as GameObject;    
                UnityEngine.AI.NavMeshAgent agent = client.GetComponent<UnityEngine.AI.NavMeshAgent>();
                agent.destination = goal.position;

                animator = client.GetComponent<Animator>();                                     // Animator Controller lines
                if (Vector3.Distance(client.transform.position, goal.position) <= 30)
                {
                    Debug.Log("here");
                    agent.;
                    animator.SetBool("Stop", true);                                             // Supposed to toggle the Stop variable and make the soldiers idle
                }

            }
            client.transform.parent = gameObject.transform;
            clientsSpawned++;
            //client.AddComponent<ClientMovement>();
            timer = resetTimer;
        }
    }

    static public void RemoveClient(Vector3 clientPosition)
    {
        spawnPositions.Add(clientPosition);
        timer = resetTimer;
        clientsSpawned--;
    }
}
