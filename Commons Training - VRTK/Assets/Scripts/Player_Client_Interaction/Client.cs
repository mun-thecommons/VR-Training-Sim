using UnityEngine;

/// <summary>
/// This script may be able to meld into another script.
/// 
/// ##Detailed
/// This script is attached to each client to keep track of which ones are in the process of asking a question. Due to its relativly small size this script may be best 
/// melded into another one to cut down on useless scripts.
/// 
/// </summary>
public class Client : MonoBehaviour
{
    public bool askingQuestion;

    void Start()
    {        
        askingQuestion = false;        
    }

    private void OnDestroy()
    {
        ClientManager.RemoveClient(transform.position);
    }
}