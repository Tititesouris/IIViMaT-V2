using UnityEngine;
using UnityEngine.Video;

public class Test360 : MonoBehaviour
{
    public GameObject video;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
            ToggleVideo();
        else if (Input.GetKeyUp(KeyCode.R)) ResetVideo();
    }

    private void ResetVideo()
    {
        video.GetComponent<VideoPlayer>().Stop();
        video.GetComponent<AudioSource>().Stop();
    }

    private void ToggleVideo()
    {
        var renderer = video.GetComponent<MeshRenderer>();
    }
}