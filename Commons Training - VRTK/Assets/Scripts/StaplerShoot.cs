using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaplerShoot : MonoBehaviour
{
    public GameObject whiteSmokePrefab;
    private GameObject whiteSmoke;
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
            MasterController.ScoreModify(0,0,-1,false,false);
            whiteSmoke = Instantiate(whiteSmokePrefab, other.transform.position, Quaternion.identity) as GameObject;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Client"))
        {
            MasterController.ScoreModify(-1,0,0,false,true);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            MasterController.ScoreModify(1, 0, 0, true, false);
            other.GetComponent<EnemyController>().TakeDamage();
            Destroy(gameObject);
        }
    }
}
