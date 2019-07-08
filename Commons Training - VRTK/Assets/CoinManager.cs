using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform coinCollectParent;

    static private List<Vector3> spawnPositions = new List<Vector3>();

    private float timer = 5f;
    private float resetTimer = 5f;

    private GameObject coinCollect;

    private int randInd;
    private Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPositions.Add(new Vector3(-1.16f, 1.25f, 1.18f));
        spawnPositions.Add(new Vector3(-0.2f, 1.25f, 3.16f));
        spawnPositions.Add(new Vector3(-2.24f, 1.25f, 1.06f));
        spawnPositions.Add(new Vector3(-3.08f, 1.25f, -0.62f));
        spawnPositions.Add(new Vector3(-2f, 1.25f, -1.87f));
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        SpawnCoin();
    }

    void SpawnCoin()
    {
        if(timer <= 0)
        {
            timer = resetTimer;
            if(spawnPositions.Count > 0)
            {
                randInd = Random.Range(0, spawnPositions.Count);
                spawnPosition = spawnPositions[randInd];
                spawnPositions.RemoveAt(randInd);
                coinCollect = Instantiate(coinPrefab, spawnPosition, coinPrefab.transform.rotation) as GameObject;
                coinCollect.transform.parent = coinCollectParent;
            }
        }
    }

    public static void CollectCoin(Vector3 position)
    {
        MasterController.coins++;
        spawnPositions.Add(position);
    }
}
