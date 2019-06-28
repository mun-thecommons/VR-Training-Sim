using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaplerManager : MonoBehaviour
{
    public GameObject staplerPrefab;
    public Transform staplerCollectParent;

    static private List<Vector3> spawnPositions = new List<Vector3> { };
    private float timer = 5f;
    private float resetTimer = 5f;
    private GameObject staplerCollect;
    private int randInd;
    private const float yPos = 1f;
    private Vector3 spawnPosition;

    private void Start()
    {
        spawnPositions.Add(new Vector3(-3.04f, yPos, -7.92f));
        spawnPositions.Add(new Vector3(-1.25f, yPos, -4.23f));
        spawnPositions.Add(new Vector3(-2.44f, yPos, -0.29f));
        spawnPositions.Add(new Vector3(0.78f, yPos, 10.77f));
        spawnPositions.Add(new Vector3(7.05f, yPos, 15.83f));
        spawnPositions.Add(new Vector3(1.7f, yPos, 21.81f));
        spawnPositions.Add(new Vector3(-6.77f, yPos, 21.81f));
        spawnPositions.Add(new Vector3(-6.77f, yPos, 30.4f));
        spawnPositions.Add(new Vector3(14.3f, yPos, -6f));
        spawnPositions.Add(new Vector3(19f, yPos, -2.4f));
        spawnPositions.Add(new Vector3(14.66f, yPos, 0.22f));
        spawnPositions.Add(new Vector3(14.66f, yPos, 5.2f));
        spawnPositions.Add(new Vector3(14.66f, yPos, -21.76f));
        spawnPositions.Add(new Vector3(9.88f, yPos, -32.3f));
        spawnPositions.Add(new Vector3(0.63f, yPos, -32.3f));
        spawnPositions.Add(new Vector3(-12.93f, yPos, -35.43f));
        spawnPositions.Add(new Vector3(-24.64f, yPos, -34.35f));
        spawnPositions.Add(new Vector3(-35.1f, yPos, -28.3f));
        spawnPositions.Add(new Vector3(-16.4f, yPos, 14.5f));
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
                staplerCollect = Instantiate(staplerPrefab, spawnPosition, staplerPrefab.transform.rotation) as GameObject;
                staplerCollect.transform.parent = staplerCollectParent;
            }
        }        
    }

    public static void CollectStapler(Vector3 position)
    {
        MasterController.staplers++;
        spawnPositions.Add(position);
    }
}
