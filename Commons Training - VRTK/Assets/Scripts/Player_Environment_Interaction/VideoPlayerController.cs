using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// The script controls the Intro Video being played/skipped
/// </summary>

public class VideoPlayerController : MonoBehaviour
{
    
    bool videoPlayed = false;
    public Text videoText;

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.RawButton.A) && !videoPlayed)
        {
            gameObject.GetComponent<VideoPlayer>().Play();
            videoPlayed = true;
            videoText.color = Color.red;
            videoText.text = "Press A to skip";
        }
        else if(OVRInput.GetDown(OVRInput.RawButton.A) && videoPlayed)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
