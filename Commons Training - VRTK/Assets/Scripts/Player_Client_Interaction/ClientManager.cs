using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ClientManager : MonoBehaviour {
   
    static private float cashBoxTimerReset = 15f;
    public GameObject cashBoxClient;

    public List<GameObject> mainDeskClients = new List<GameObject>();
    private GameObject mainDeskClient;
    private Vector3 spawnPositionDesk;
    static public Vector3 deskGoal;
    public static bool deskClient = false;

    public List<GameObject> mcClient = new List<GameObject>();
    public List<GameObject> walkingClient = new List<GameObject>();
    static private List<Vector3> spawnPositions = new List<Vector3>();
    private Vector3 spawnPosition;
    private Vector3 spawnPositionWalk;
    public int numOfWalkingClient = 3;
    static public List<Vector3> destinationPositions = new List<Vector3>();

    private GameObject client;
    private GameObject npcClient;
    static private float timer = 5f;
    static private float resetTimer = 5f;
    private int randInd;
    private float yPos = 0.08f;
    static public int clientsSpawned = 0;
    private Vector3 goal;
    private NavMeshAgent agent;

    private void Start()
    {
        
        spawnPositionWalk = new Vector3(-34.6f, yPos, 2.49f);
        spawnPositionDesk = new Vector3(-27.957f, yPos, -35.9f);
        spawnPositions.Add(new Vector3(-30.28f, yPos, -35.9f));
        spawnPositions.Add(new Vector3(-27.957f, yPos, -35.9f));
        spawnPositions.Add(new Vector3(-27.73f, yPos, -38.6f));
        spawnPositions.Add(new Vector3(-28.957f, yPos, -35.9f));
        destinationPositions.Add(new Vector3(2.5f, yPos, -13.68f));
        destinationPositions.Add(new Vector3(-7.84f, yPos, -22.32f));
        destinationPositions.Add(new Vector3(-21.5f, yPos, -12.5f));
        destinationPositions.Add(new Vector3(11.7f, yPos, -22.32f));
        //destinationPositions.AddRange(GameObject.FindGameObjectsWithTag("Destination"));
        Debug.Log("number of destination points is "+destinationPositions.Count);
    }

    void Update ()
    {
        timer -= Time.deltaTime;
        SpawnClient();
	}

    void SpawnClient()
    {
        if (!deskClient && Level.level > 1)
        {
            int deskClientInd = Random.Range(0, mainDeskClients.Count);
            mainDeskClient = Instantiate(mainDeskClients[deskClientInd], spawnPositionDesk, mainDeskClients[deskClientInd].transform.rotation) as GameObject;
            deskGoal = new Vector3(-3.6f, yPos, -12.0f);
            mainDeskClient.GetComponent<NavMeshAgent>().destination = deskGoal;
            deskClient = true;            
        }
        if (timer <= 0 && destinationPositions.Count > 0 && spawnPositions.Count > 0)
        {
            randInd = Random.Range(0, spawnPositions.Count);
            spawnPosition = spawnPositions[randInd];
            randInd = Random.Range(0, destinationPositions.Count);
            goal = destinationPositions[randInd];
            spawnPositions.RemoveAt(randInd);
            destinationPositions.RemoveAt(randInd);

            int mcClientInd = Random.Range(0, mcClient.Count);
            client = Instantiate(mcClient[mcClientInd], spawnPosition, mcClient[mcClientInd].transform.rotation) as GameObject;
            client.GetComponent<NavMeshAgent>().destination = goal;

            
            int walkingInd = Random.Range(0, walkingClient.Count);
            npcClient = Instantiate(walkingClient[walkingInd], spawnPositionWalk, walkingClient[walkingInd].transform.rotation) as GameObject;

            client.transform.parent = gameObject.transform;
            npcClient.transform.parent = gameObject.transform;
            clientsSpawned++;
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
