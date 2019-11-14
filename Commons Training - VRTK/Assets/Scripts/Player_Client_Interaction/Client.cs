using UnityEngine;

public class Client : MonoBehaviour
{
    public Transform player;
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