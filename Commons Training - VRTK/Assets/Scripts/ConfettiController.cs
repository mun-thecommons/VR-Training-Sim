using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiController : MonoBehaviour
{
    // Start is called before the first frame update

    private ParticleSystem[] confettis;

    public int amountOfEachColor = 50;

    private GameObject player;

    void Start()
    {
        confettis = GetComponentsInChildren<ParticleSystem>(); // Get all confetti colours

        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void throwConfetti()
    {
        Vector3 pos = player.transform.position;
        pos.y -= 2; // So the confetti doesn't spawn on the player's head
        transform.position = pos; // Deploy confetti at player's current pos
        foreach (ParticleSystem p in confettis) // Throw an equal amout of all colours
        {
            p.Emit(amountOfEachColor);
        }
    }
}
