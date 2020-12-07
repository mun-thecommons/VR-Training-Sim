using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Manages the amount of collectibles allowed in the scene
/// 
/// ##Detailed
/// Stores all the data pertaining to the collectibles within the scene. It stores the spawn locations, the different forms of collectibles as well as their prefab Gameobjects. 
/// Each update will determine if an item needs to be spawned based upon the amount collected by the player. 
/// 
/// Collectibles include:
/// - Coins
/// - USB
/// - Scrap Paper
/// - Staplers
/// - Trash (3 types: Metal, Plastic, Base)
/// 
/// @note The functions included here are used in different scripts
/// 
/// @see ScrapPaperController; TrashController; CoinController; StaplerProjectile
/// </summary>
public class CollectibleManager : MonoBehaviour
{
    //STAPLERS
    public GameObject staplerPrefab;


    //COINS
    public GameObject coinPrefab;


    //TRASH
    public GameObject baseTrashPrefab;
    public GameObject metalTrashPrefab;
    public GameObject plasticTrashPrefab;
    public static int numOfTrash = 0;
    private int typeOfTrash;
    private List<GameObject> trashType = new List<GameObject>();

    //USBS
    public GameObject usbPrefab;


    //SCRAP PAPERS
    public GameObject scrapPaperPrefab;


    //COLLECTS OF ALL 5
    private GameObject coinCollect;
    private GameObject staplerCollect;
    private GameObject usbCollect;
    private GameObject scrapPaperCollect;
    private GameObject trashCollect;
    public Transform collectiblesCollectParent;

    private int randInd;
    private int randCollectible;
    
    private float yPos = 1.03f;

    public float timerLength = 5f;
    private float timer = 5f;

    private Vector3 spawnPosition;

    public static List<Vector3> spawnPositions = new List<Vector3>();
    private GameObject[] spawnObjects;

    private static List<GameObject> collectibles = new List<GameObject>();
    private static List<GameObject> collectiblesPrefabs = new List<GameObject>();

    /*****************************
     * Designates what will be involved in each of the arrays
     * 
     * ##Detailed
     * Adds in the neccessary Gameobjects and prefabs into the different arrays for instantiation.
     * Also, upon start the different Spawnlocations are determined and placed within an array for when an item is to be instantiated .
     * 
     * ***************************/
    void Start()
    {
        timer = timerLength;

        trashType.Add(baseTrashPrefab);
        trashType.Add(metalTrashPrefab);
        trashType.Add(plasticTrashPrefab);

        collectibles.Add(coinCollect);
        collectibles.Add(staplerCollect);
        collectibles.Add(usbCollect);
        collectibles.Add(scrapPaperCollect);
        collectibles.Add(trashCollect);
        //Debug.Log("collectibles first item is "+collectibles[0].gameObject.name);
        collectiblesPrefabs.Add(coinPrefab);
        collectiblesPrefabs.Add(staplerPrefab);
        collectiblesPrefabs.Add(usbPrefab);
        collectiblesPrefabs.Add(scrapPaperPrefab);
        collectiblesPrefabs.Add(baseTrashPrefab);
        //Debug.Log("collectible prefab last item is "+collectiblesPrefabs[4].name);

        spawnObjects = GameObject.FindGameObjectsWithTag("SpawnPos"); // Get all SpawnPos objects. These are used to designate where collectibles are allowed to spawn

        foreach (GameObject obj in spawnObjects)
        {
            spawnPositions.Add(new Vector3(obj.transform.position.x, yPos, obj.transform.position.z)); // Add a new spawn location based on each spawnPos object
        }
    }

    /*****************************
     * Based on the remaining time of the timer, the update will run the SpawnCollectibles function
     * 
     * ***************************/
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnCollectible();
            timer = timerLength;
        }
    }

    /*****************************
     * Spawns collectibles in random predetermined locations dependent on a timer
     * 
     * ##Detailed
     * After an amount of time has passed a random collectible will spawn in one of the predetermined locations. 
     * The type of collectible spawned will depend upon the amount currently collected by the player, therefore it will be more even distribution of items.
     * 
     * As items are spawned into locations those locations are removed from the location Array as to not have overlap of items
     * 
     * ***************************/
    private void SpawnCollectible() 
    {
        if (spawnPositions.Count > 0)
        {
           
            randInd = Random.Range(0, spawnPositions.Count);
            randCollectible = Random.Range(0, collectibles.Count);
            spawnPosition = spawnPositions[randInd];
            spawnPositions.RemoveAt(randInd);
                         
            if (randCollectible == 4)
            {
                numOfTrash++;
                typeOfTrash = Random.Range(0, trashType.Count);
                collectibles[randCollectible] = Instantiate(trashType[typeOfTrash], spawnPosition, Quaternion.identity);
            }
            else
            {
                collectibles[randCollectible] = Instantiate(collectiblesPrefabs[randCollectible], spawnPosition, collectiblesPrefabs[randCollectible].transform.rotation) as GameObject;          
            }
            
            collectibles[randCollectible].transform.parent = collectiblesCollectParent;

        }
        
    }

}
