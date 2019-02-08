using UnityEngine;

public class Client_2 : MonoBehaviour {

    public Canvas canvas1;
    public Canvas canvas2;
    public Transform player;
    public GameObject client2;

    public static bool askingQuestion;

    private void Start()
    {
        canvas1.enabled = false;
        canvas2.enabled = false;
        askingQuestion = false;
    }

    private void Update()
    {
        transform.rotation = player.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Client_2"))
        {
            canvas1.enabled = true;
            canvas2.enabled = true;
            askingQuestion = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Client_2"))
        {
            canvas1.enabled = false;
            canvas2.enabled = false;
            askingQuestion = false;

            if (QuestionInput.questionAnswered)
            {
                Vector3 move = new Vector3(100.0f, 0, 0);
                client2.transform.position += move;
                QuestionInput.questionAnswered = false;
            }
        }
    }
}
