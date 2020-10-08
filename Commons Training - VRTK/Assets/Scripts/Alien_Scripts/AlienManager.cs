using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Control the spawning of aliens at random locations in the scene
/// </summary>
public class AlienManager : MonoBehaviour
{       
    public GameObject alienPrefab;
    private GameObject alien;
    static public List<Vector3> spawnPositions = new List<Vector3>();   
    private Vector3 spawnPosition;  
    private float yPos = 0.3f;
    public static int howManyAlien = 3;
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

    /******************************
     * Spawn Aliens to maintain a set number of aliens on the scene
     * The Alien Game Objects are organized under a parent game object
     * @note decide if spawning aliens only occurs when the lab satisfaction score is too low
     * ****************************/
    void SpawnAlien()
    {
/*        if(*//*MasterController.labSatisfaction < 1000&& *//* GameObject.FindGameObjectsWithTag("Alien").Length <= howManyAlien)
        {
            randInd = Random.Range(0, spawnPositions.Count - 1);
            spawnPosition = spawnPositions[randInd];
            spawnPositions.RemoveAt(randInd);
            alien = Instantiate(alienPrefab, spawnPosition, alienPrefab.transform.rotation) as GameObject;
            alien.transform.parent = alientParent.transform;
        }*/
    }

    /******************************
     * When an alien is shot, the current position of the alien is vacated for spawning new aliens
     * ****************************/
    static public void ShootAlien(Vector3 x)
    {
        spawnPositions.Add(x);
    }
} 
