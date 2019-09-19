using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienManager : MonoBehaviour
{       
    public GameObject alien;  
    static public List<Vector3> destinationPositions = new List<Vector3>(); 
    static public List<Vector3> spawnPositions = new List<Vector3>();   
    private Vector3 spawnPosition;  
    private Vector3 goal;
    private NavMeshAgent agent;
    private float yPos = 0.08f;
    private int howMany = 0;
    private int randInd;

    // Start is called before the first frame update
    void Start()
    {
        spawnPositions.Add(new Vector3(35.28f, yPos, -17.0f));
        spawnPositions.Add(new Vector3(-12.57f, yPos, -19.9f));
        spawnPositions.Add(new Vector3(-38.73f, yPos, 12.0f));
        spawnPositions.Add(new Vector3(-10.957f, yPos, -33.5f));
        destinationPositions.Add(new Vector3(25.7f, yPos, -3.68f));
        destinationPositions.Add(new Vector3(-7.84f, yPos, -20.12f));
        destinationPositions.Add(new Vector3(-11.5f, yPos, -18.5f));
        destinationPositions.Add(new Vector3(19.7f, yPos, -2.32f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void SpawnAlien()
    {
        if(MasterController.labSatisfaction < 1000 && howMany < 5)
        {
            randInd = Random.Range(0, spawnPositions.Count);
            spawnPosition = spawnPositions[randInd];
            randInd = Random.Range(0, destinationPositions.Count);
            goal = destinationPositions[randInd];
            spawnPositions.RemoveAt(randInd);
            destinationPositions.RemoveAt(randInd);
            alien = Instantiate(invader, spawnPosition, invader.transform.rotation) as GameObject;
            alien.GetComponent<NavMeshAgent>().destination = goal;
            howMany++;
        }
    }*/
} 
