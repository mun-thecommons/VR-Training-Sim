using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script allows the player to quit the game by interacting with a Game Object
/// </summary>
public class ExitGame : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            Application.Quit();
            Debug.Log("suppose to quit");
        }
    }
}
