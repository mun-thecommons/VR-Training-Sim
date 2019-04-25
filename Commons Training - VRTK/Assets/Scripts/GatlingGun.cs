using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingGun : MonoBehaviour {


    private AudioSource gutlingGunSound;
	// Use this for initialization
	void Start () {
        GetComponent<Animation>().enabled = false;
        gutlingGunSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            GetComponent<Animation>().enabled = true;
            gutlingGunSound.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            GetComponent<Animation>().enabled = false;
            gutlingGunSound.Pause();
        }
    }
}
