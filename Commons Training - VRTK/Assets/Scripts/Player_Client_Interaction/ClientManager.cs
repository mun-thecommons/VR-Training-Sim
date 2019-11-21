﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ClientManager : MonoBehaviour {
   
    static private float cashBoxTimerReset = 15f;

    public List<GameObject> cashBoxClients = new List<GameObject>();
    private GameObject cashBoxClient;
    private Vector3 spawnPositionCash;
    static public Vector3 cashGoal;
    public static bool cashClient = false;

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
        spawnPositionCash = new Vector3(-13f, yPos, -20);    
        spawnPositionWalk = new Vector3(-22f, yPos, -40f);
        spawnPositionDesk = new Vector3(-15f, yPos, -4f);
        spawnPositions.Add(new Vector3(-22f, yPos, -18f));
        spawnPositions.Add(new Vector3(3f, yPos, 7f));
        spawnPositions.Add(new Vector3(-2f, yPos, 20f));
        spawnPositions.Add(new Vector3(-22f, yPos, 17f));
        destinationPositions.Add(new Vector3(-2f, yPos, -14f));
        destinationPositions.Add(new Vector3(-44f, yPos, -17f));
        destinationPositions.Add(new Vector3(18f, yPos, 21f));
        destinationPositions.Add(new Vector3(-6f, yPos, -23f));
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
        if (!cashClient && Level.level > 0)
        {
            int cashClientInd = Random.Range(0, cashBoxClients.Count);
            cashBoxClient = Instantiate(cashBoxClients[cashClientInd], spawnPositionCash, cashBoxClients[cashClientInd].transform.rotation) as GameObject;
            cashGoal = new Vector3(7.0f, yPos, -10.0f);
            cashBoxClient.GetComponent<NavMeshAgent>().destination = cashGoal;
            cashClient = true;
        }

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
