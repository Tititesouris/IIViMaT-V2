using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Test360 : MonoBehaviour {

    public GameObject video;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            ToggleVideo();
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            ResetVideo();
        }


    }

    public void ResetVideo()
    {
        video.GetComponent<VideoPlayer>().Stop();
        video.GetComponent<AudioSource>().Stop();
    }

    public void ToggleVideo()
    {
        MeshRenderer renderer = video.GetComponent<MeshRenderer>();
    }
}
