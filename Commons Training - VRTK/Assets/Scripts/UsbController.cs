﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsbController : MonoBehaviour {
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update () {
       // transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    //USB and USB box collision logic
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("USBox"))
        {
            Debug.Log("collision worked");
            player.GetComponent<Audio>().correctSound();//usbCollectedSound.Play();      //USB signal is played
            QuestionInput.ScoreIncrement();//played and the player gets a score point      
    
            Destroy(gameObject);
   
   
        }

    }


    /*
    private void OnTriggerEnter(Collider other)
    {

        QuestionInput.ScoreIncrement();
        FindObjectOfType<Audio>().correctSound();
        Destroy(gameObject);
    }
    */
}
