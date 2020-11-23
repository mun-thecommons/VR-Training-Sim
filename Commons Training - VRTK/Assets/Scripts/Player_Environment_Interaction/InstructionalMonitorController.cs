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
    public string videoName;        /*!< @brief String used to identify what video this monitor is playing. Used to control game state  */

    private GameObject player;

    public AudioSource monitorAudio;
    public AudioClip pauseAudio;

    private bool pausedByPlayer = true; /*!< @brief Used to determine if the video ended naturally or by being paused by player action  */

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
        if (videoPlayer.isPlaying && Vector3.Magnitude(transform.position - player.transform.position) > maxDistance)
        {
            videoPlayer.Pause();
            pausedByPlayer = true;
        }

        if (videoPlayer.isPaused && !pausedByPlayer) // The video is paused by reaching the end
        {
            if (!GameManager.watchedVideos.Contains(videoName)) // Only append this video to the list once
            {
                Debug.Log("video watched");
                GameManager.watchedVideos.Add(videoName); // Add this video to the watched videos list
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand") && GameManager.monitorsOn) // Make sure that the monitors are on before trying to play video
        {
            if (GameManager.monitorsOn)
            {
                if (pausedByPlayer)
                {
                    videoPlayer.Play();
                    pausedByPlayer = false;
                }
                else
                {
                    videoPlayer.Pause();
                    pausedByPlayer = true;
                }
                monitorAudio.PlayOneShot(pauseAudio, 1); // Play beep to notify player that they paused/resumed the video
            }
            else
            {
                GameManager.playerAudioSource.PlayOneShot(GameManager.deniedAudioClip);
            }
        }
    }
}
