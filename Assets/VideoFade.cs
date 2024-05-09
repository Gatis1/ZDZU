using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoFade : MonoBehaviour
{
    public float hideTime = 5f; // Time in seconds after which the video player will be disabled
    private float timer = 0f;
    private bool hasHidden = false;
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Get the VideoPlayer component attached to the same GameObject
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if it's time to hide the video player
        if (timer >= hideTime && !hasHidden)
        {
            // Disable the video player
            videoPlayer.enabled = false;
            hasHidden = true;
        }
    }
}
