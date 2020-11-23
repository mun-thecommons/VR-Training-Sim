using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control the button that turns all of the computers on
/// 
/// ##Detailed
/// This script turns all computers on by updating the monitorsOn variable in the GameManager
/// 
/// </summary>
public class ComputerButtonController : MonoBehaviour
{

    public float blinkInterval = 1.0f;
    private float blinkTimer = 0.0f;

    public GameObject button;
    private Renderer buttonRenderer;

    public Light light;

    private Animator buttonAnim;
    private bool pressed = false;

    public AudioClip startupSound;


    // Start is called before the first frame update
    void Start()
    {
        buttonRenderer = button.GetComponent<Renderer>();
        buttonAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        blinkTimer += Time.deltaTime;

        // Code to blink red light on and off
        if (!pressed && blinkTimer >= blinkInterval)
        {
            if (buttonRenderer.material.color == Color.red)
            {
                buttonRenderer.material.color = Color.black;
                light.color = Color.black;
            }
            else
            {
                buttonRenderer.material.color = Color.red;
                light.color = Color.red;
            }

            blinkTimer = 0.0f;
        }
        else if (pressed && buttonRenderer.material.color != Color.green)
        {
            buttonRenderer.material.color = Color.green;
            light.color = Color.green;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pressed && other.CompareTag("Hand")) // Make sure the player has the vest before doing anything
        {
            if (GameManager.hasVest)
            {
                GameManager.playerAudioSource.PlayOneShot(startupSound);
                GameManager.monitorsOn = true;

                buttonAnim.SetTrigger("Press");
                pressed = true;
            }
            else
            {
                GameManager.playerAudioSource.PlayOneShot(GameManager.deniedAudioClip);
            }
        }
            
    }
}
