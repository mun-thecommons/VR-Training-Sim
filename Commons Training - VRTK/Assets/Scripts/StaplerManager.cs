using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaplerManager : MonoBehaviour
{
    public GameObject staplerPrefab;
    public Transform staplerThrowParent;

    static private List<Vector3> spawnPositions = new List<Vector3> { };
    private float timer = 5f;
    private float resetTimer = 5f;
    private GameObject stapler;
    private int randInd;
    private const float yPos = 1f;
    private Vector3 spawnPosition;

    private void Start()
    {
        spawnPositions.Add(new Vector3(3.66f, yPos, -1.768f));
        spawnPositions.Add(new Vector3(5.724f, yPos, -0.125f));
        spawnPositions.Add(new Vector3(-0.125f, yPos, 5.724f));
        spawnPositions.Add(new Vector3(-1.768f, yPos, 3.66f));
    }

    void Update()
    {
        timer -= Time.deltaTime;
        SpawnStapler();
    }

    void SpawnStapler()
    {
        if(timer <= 0)
        {
            timer = resetTimer;
            if (spawnPositions.Count > 0)
            {
                randInd = Random.Range(0, spawnPositions.Count);
                spawnPosition = spawnPositions[randInd];
                spawnPositions.RemoveAt(randInd);
                stapler = Instantiate(staplerPrefab, spawnPosition, staplerPrefab.transform.rotation) as GameObject;
                stapler.transform.parent = staplerThrowParent;
            }
        }        
    }

    public static void CollectStapler(Vector3 position)
    {
        spawnPositions.Add(position);
    }

}
