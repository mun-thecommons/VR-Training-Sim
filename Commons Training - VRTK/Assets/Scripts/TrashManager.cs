using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour
{
    public GameObject baseTrashPrefab;
    public GameObject metalTrashPrefab;
    public GameObject plasticTrashPrefab;
    public Transform trashCollectParent;

    public static int numOfTrash = 0;
    private static List<Vector3> spawnPositions = new List<Vector3>();
    private Vector3 spawnPosition;
    private List<GameObject> trashType = new List<GameObject>();
    private float timer = 10f;
    private float resetTimer = 10f;
    private GameObject trashCollect;
    private int typeOfTrash;
    private int randPosInd;
    private float yPos = 1.03f;

    // Start is called before the first frame update
    void Start()
    {
        trashType.Add(baseTrashPrefab);
        trashType.Add(metalTrashPrefab);
        trashType.Add(plasticTrashPrefab);

        spawnPositions.Add(new Vector3(-12.16f, yPos, -5.72f));
        spawnPositions.Add(new Vector3(5.01f, yPos, -2.97f));
        spawnPositions.Add(new Vector3(-8.71f, yPos, -1.22f));
        spawnPositions.Add(new Vector3(-10.85f, yPos, 1.96f));
        spawnPositions.Add(new Vector3(0.73f, yPos, 3.39f));
        spawnPositions.Add(new Vector3(-2.11f, yPos, -6.13f));
    }   

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        SpawnTrash();
    }

    void SpawnTrash()
    {
        if(timer <= 0f)
        {
            timer = resetTimer;
            if(spawnPositions.Count > 0)
            {
                numOfTrash++;
                typeOfTrash = Random.Range(0, trashType.Count);
                randPosInd = Random.Range(0, spawnPositions.Count);
                spawnPosition = spawnPositions[randPosInd];
                spawnPositions.RemoveAt(randPosInd);
                trashCollect = Instantiate(trashType[typeOfTrash], spawnPosition, Quaternion.identity);
                trashCollect.transform.parent = trashCollectParent;
            }
        }
    }

    public static void CollectTrash(Vector3 position)
    {
        spawnPositions.Add(position);
        numOfTrash--;
    }
}
