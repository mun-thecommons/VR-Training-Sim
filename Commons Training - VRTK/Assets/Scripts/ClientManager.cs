using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour {
   
    static private float cashBoxTimerReset = 15f;
    public GameObject cashBoxClient;
    public GameObject mcClient;
    static private List<Vector3> spawnPositions = new List<Vector3> { };
    private Vector3 spawnPosition;
    private Vector3 destinationPosition;
    private GameObject client;
    private float timer = 5f;
    private float resetTimer = 5f;
    private int randInd;
    private float yPos = 1.26f;

    private void Start()
    {
        spawnPositions.Add(new Vector3(5.25f, yPos, -13.186f));
        spawnPositions.Add(new Vector3(-4.48f, yPos, -20.48f));
        spawnPositions.Add(new Vector3(3.78f, yPos, -11.73f));
        destinationPosition =new Vector3(-26.8f,1.26f, -31.4f);


    }

    void Update ()
    {
        timer -= Time.deltaTime;
        SpawnClient();       
	}

    void SpawnClient()
    {
        if (timer <= 0)
        {
            timer = resetTimer;
            if (spawnPositions.Count > 0)
            {
                randInd = Random.Range(0, spawnPositions.Count);
                spawnPosition = spawnPositions[randInd];
                spawnPositions.RemoveAt(randInd);
                if (Random.Range(0, 2) == 0)
                {
                    client = Instantiate(cashBoxClient, spawnPosition, cashBoxClient.transform.rotation) as GameObject;
                }
                else
                {
                    client = Instantiate(mcClient, spawnPosition, mcClient.transform.rotation) as GameObject;
                }
                client.transform.parent = transform;
                client.AddComponent<ClientMovement>();

            }
        }
    }
}
