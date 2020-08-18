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

    private float timer = 5f;
    private float resetTimer = 5f;
    private Vector3 spawnPosition;
    private static List<Vector3> spawnPositions = new List<Vector3>();
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
        spawnPositions.Add(new Vector3(-12.16f, yPos, -5.72f));
        spawnPositions.Add(new Vector3(5.01f, yPos, -2.97f));
        spawnPositions.Add(new Vector3(-8.71f, yPos, -1.22f));
        spawnPositions.Add(new Vector3(-10.85f, yPos, 1.96f));
        spawnPositions.Add(new Vector3(0.73f, yPos, 3.39f));
        spawnPositions.Add(new Vector3(-2.11f, yPos, -6.13f));
        spawnPositions.Add(new Vector3(-1.16f, 1.25f, 1.18f));
        spawnPositions.Add(new Vector3(-0.2f, 1.25f, 3.16f));
        spawnPositions.Add(new Vector3(-2.24f, 1.25f, 1.06f));
        spawnPositions.Add(new Vector3(-3.08f, 1.25f, -0.62f));
        spawnPositions.Add(new Vector3(-2f, 1.25f, -1.87f));
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

    /*****************************
     * Based on the remaining time of the timer, the update will run the SpawnCollectibles function
     * 
     * ***************************/
    void Update()
    {
        timer -= Time.deltaTime;
        SpawnCOllectibles();

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
    void SpawnCOllectibles() {
        if (timer <= 0f)
        {
            timer = resetTimer;
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

    /*****************************
    * Used to increment amount of Trash collected
    * 
    * @note Used within the TrashController
    * ***************************/
    public static void CollectTrash(Vector3 position)
    {
        spawnPositions.Add(position);
        numOfTrash--;
        if(Level.level == 2)
        {
            Level.level2Trash = true;
        }
    }
    /*****************************
    * Used to increment amount of Coins collected
    * 
    * @note Used within the CoinController
    * ***************************/
    public static void CollectCoin(Vector3 position)
    {
        MasterController.coins++;
        spawnPositions.Add(position);
    }
    /*****************************
     * Used to increment amount of Staplers collected
     * 
     * @note Used within the StaplerProjectile
     * ***************************/
    public static void CollectStapler(Vector3 position)
    {
        StaplerProjectile.staplers++;
        spawnPositions.Add(position);
    }
    /*****************************
     * Used to increment amount of Scrappaper collected
     * 
     * @note Used within the ScrapPaperController
     * ***************************/
    public static void CollectScrapPaper(Vector3 position)
    {
        MasterController.scrapPaper++;
        spawnPositions.Add(position);
    }



}
