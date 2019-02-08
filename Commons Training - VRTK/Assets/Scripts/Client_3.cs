using UnityEngine;

public class Client_3 : MonoBehaviour
{

    public Canvas canvas1;
    public Canvas canvas2;
    public Transform player;
    public GameObject client3;

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
        if (other.CompareTag("Client_3"))
        {
            canvas1.enabled = true;
            canvas2.enabled = true;
            askingQuestion = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Client_3"))
        {
            canvas1.enabled = false;
            canvas2.enabled = false;
            askingQuestion = false;

            if (QuestionInput.questionAnswered)
            {
                Vector3 temp = new Vector3(100.0f, 0, 0);
                client3.transform.position += temp;
                QuestionInput.questionAnswered = false;
            }
        }
    }
}
