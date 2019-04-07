using UnityEngine;

public class Client : MonoBehaviour
{

    public Transform player;
    public bool askingQuestion;

    void Start()
    {        
        askingQuestion = false;
        //transform.rotation = player.rotation;
    }

    private void Update()
    {
        //transform.rotation = player.rotation;
    }
    
}