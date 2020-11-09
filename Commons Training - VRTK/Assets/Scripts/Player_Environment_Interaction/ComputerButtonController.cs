using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (!pressed && other.CompareTag("Hand"))
        {
            GameManager.playerAudioSource.PlayOneShot(startupSound);
            GameManager.monitorsOn = true;

            buttonAnim.SetTrigger("Press");
            pressed = true;
        }
    }
}
