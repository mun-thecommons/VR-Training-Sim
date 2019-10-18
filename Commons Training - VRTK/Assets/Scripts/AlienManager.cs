using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienManager : MonoBehaviour
{       
    public GameObject alien; 
    static public List<Vector3> spawnPositions = new List<Vector3>();   
    private Vector3 spawnPosition;  
    private float yPos = 1.0f;
    public static int howManyAlien = 0;
    private int randInd;
    public Transform alientParent;

    // Start is called before the first frame update
    void Start()
    {
        spawnPositions.Add(new Vector3(-34.8f, yPos, -13.0f));
        spawnPositions.Add(new Vector3(18.9f, yPos, -25.6f));
        spawnPositions.Add(new Vector3(-24.3f, yPos, -33.2f));
        spawnPositions.Add(new Vector3(-15.3f, yPos, 16.5f));
        spawnPositions.Add(new Vector3(-1.2f, yPos, -10.1f));
    }

    // Update is called once per frame
    void Update()
    {
        SpawnAlien();
    }

    void SpawnAlien()
    {
        if(/*MasterController.labSatisfaction < 1000 &&*/ howManyAlien <= 5)
        {
            randInd = Random.Range(0, spawnPositions.Count);
            spawnPosition = spawnPositions[randInd];
            spawnPositions.RemoveAt(randInd);
            alien = Instantiate(alien, spawnPosition, alien.transform.rotation) as GameObject;
            alien.transform.parent = alientParent.transform;
            howManyAlien++;
        }
    }
} 
