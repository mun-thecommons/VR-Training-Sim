using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Logic used for the Campus card of the CashClient
/// 
/// ##Detailed
/// This script houses logic used when the Player is helping the CashClient load money onto their account with the Blackbox.
/// 
/// </summary>
public class CampusCard : MonoBehaviour
{
    [HideInInspector]
    public bool expired;
    public GameObject player;
    public GameObject parent;
    Vector3 cardOriginalPosition;

    // Use this for initialization
    void Start()
    {
        SetState();
    }
    
    void Update()
    {
        if( parent.GetComponent<NavMeshAgent>().isStopped && !GetComponent<MeshRenderer>().enabled)
            {
                cardOriginalPosition = transform.position;
            }

       // CheckDistance();

        if (CashClient.cardChecked && !GetComponent<MeshRenderer>().enabled)
            {
                transform.parent = GameObject.Find("CashBoxClientFemaleClient(Clone)").transform;
                Debug.Log("Parent of CampusCard: " + transform.parent);
            }
    }

    /*******************************
     * The state of the Campus Card determines the Scenario
     * 
     * ##Detailed
     * This function works in Tandem with the CashBox script. Depdendent on the state of the card will choose what message is shown on the Cashbox. 
     * 0 or 2 = There is $100 on the Account
     * 1 = The Campus Card is Expired
     * 
     * *****************************/
    void SetState()
    {
        if(Random.Range(0,2) == 1)
        {
            expired = true;
        }
        else
        {
            expired = false;
        }
    }

    /*************************************
     * Checks how far the player has taken the CampusCard. 
     * 
     * ##Detailed
     * once the player has taken the Card so far past the Client, they may let go of the card and it will Autom,atically return to the client. This way they
     * will not have to worry about accidentally dropping it somewhere by mistake.
     * 
     * ************************************/
    void CheckDistance()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 3f && GetComponent<MeshRenderer>().enabled)
        {
            Debug.Log("Let go of card");
            transform.position = cardOriginalPosition;
        }   
        
    }

    /************************
     * When the Hand collides with the card the Rotator Script is disabled as to prevent errors.
     * 
     * **************************/
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            GetComponent<Rotator>().enabled = false;
            transform.parent = GameObject.Find("CashBox").transform;
            Debug.Log("Parent of CampusCard: " + transform.parent);
        }
    }
}
