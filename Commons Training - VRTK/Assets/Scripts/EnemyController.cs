using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int healthPoint = 10;

    public void TakeDamage(int damage = 1)
    {
        healthPoint -= damage;
        if(healthPoint <= 0)
            {
                Destroy(gameObject);
            }
    }
}
