using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// Control the monitors that display training videos
/// 
/// ##Detailed
/// This script plays and pauses videos based on the player's actions
/// 
/// </summary>
public class InstructionalMonitorController : MonoBehaviour
{

    public VideoPlayer videoPlayer; /*!< @brief The Component that controls video playback  */
    public GameObject  videoScreen; /*!< @brief The GameObject that shows the video (replaces object's material)  */

    private GameObject player;

    public AudioSource monitorAudio;
    public AudioClip pauseAudio;

    private bool pausedByTouch = true; /*!< @brief One way to pause the video is by touching the monitor  */

    public float maxDistance = 5; /*!< @brief Another way to pause the video is by walking too far away  */

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.monitorsOn != videoScreen.activeSelf) // Make sure that the monitors are on before showing anything on the screen
        {
            videoScreen.SetActive(GameManager.monitorsOn);
        }

        // Handle video pausing if the player walks out of range
        if (!pausedByTouch && videoPlayer.isPlaying && Vector3.Magnitude(transform.position - player.transform.position) > maxDistance)
        {
            videoPlayer.Pause();
        }
        else if (!pausedByTouch && videoPlayer.isPaused && Vector3.Magnitude(transform.position - player.transform.position) < maxDistance)
        {
            videoPlayer.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand") && GameManager.monitorsOn) // Make sure that the monitors are on before trying to play video
        {
            if (pausedByTouch)
            {
                videoPlayer.Play();
                pausedByTouch = false;
            }
            else
            {
                videoPlayer.Pause();
                pausedByTouch = true;
            }
            monitorAudio.PlayOneShot(pauseAudio,1); // Play beep to notify player that they paused/resumed the video
        }
    }
}
