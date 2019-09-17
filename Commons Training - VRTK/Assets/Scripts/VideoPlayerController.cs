using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoPlayerController : MonoBehaviour
{
    bool videoPlayed = false;


    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.RawButton.A) && !videoPlayed)
        {
            gameObject.GetComponent<VideoPlayer>().Play();
            videoPlayed = true;
        }
        else if(OVRInput.GetDown(OVRInput.RawButton.A) && videoPlayed)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
