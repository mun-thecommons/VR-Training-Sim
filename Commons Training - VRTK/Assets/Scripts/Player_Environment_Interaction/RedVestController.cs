using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedVestController : MonoBehaviour
{

    public AudioClip vestTouch;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            GameManager.hasVest = true;
            GameManager.playerAudioSource.PlayOneShot(vestTouch, 1);
            Destroy(this.gameObject);
        }
    }
}
