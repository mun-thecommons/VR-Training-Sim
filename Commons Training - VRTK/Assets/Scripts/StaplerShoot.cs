using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaplerShoot : MonoBehaviour
{
    public float timeDestroy = 2.0f;
    public float speed = 2f;
    private Vector3 forward;

    private void Start()
    {
        Destroy(gameObject, timeDestroy);
        forward = transform.forward;
    }
    void Update()
    {
        transform.position += forward * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monitor") || other.CompareTag("Printer"))
        {
            PlayerUIScore.ScoreModify(0,0,-1,false,false);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Client"))
        {
            PlayerUIScore.ScoreModify(-1,0,0,false,false);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            PlayerUIScore.ScoreModify(1, 0, 0, true, false);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
