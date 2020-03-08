using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    void CheckDistance()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 3f && GetComponent<MeshRenderer>().enabled)
        {
            Debug.Log("Let go of card");
            transform.position = cardOriginalPosition;
        }   
        
    }

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
