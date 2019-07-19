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
    static public List<Vector3> destinationPositions = new List<Vector3>();
    private GameObject client;
    static private float timer = 2.5f;
    static private float resetTimer = 5f;
    private int randInd;
    private float yPos = 0.08f;
    static public int clientsSpawned = 0;
    private Vector3 goal;   

    private void Start()
    {    
        spawnPositions.Add(new Vector3(-30.28f, yPos, -35.9f));
        spawnPositions.Add(new Vector3(-27.957f, yPos, -35.9f));
        spawnPositions.Add(new Vector3(-27.73f, yPos, -38.6f));
        spawnPositions.Add(new Vector3(-28.957f, yPos, -35.9f));
        destinationPositions.Add(new Vector3(2.5f, yPos, -13.68f));
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
        if (timer <= 0 && clientsSpawned <= destinationPositions.Count && spawnPositions.Count > 0)
        {
            randInd = Random.Range(0, spawnPositions.Count);
            spawnPosition = spawnPositions[randInd];
            randInd = Random.Range(0, destinationPositions.Count);
            goal = destinationPositions[randInd];
            spawnPositions.RemoveAt(randInd);
            destinationPositions.RemoveAt(randInd);
            
            client = Instantiate(mcClient, spawnPosition, mcClient.transform.rotation) as GameObject;
            client.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = goal;

            client.transform.parent = gameObject.transform;
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
