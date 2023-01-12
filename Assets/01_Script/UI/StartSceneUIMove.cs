using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class StartSceneUIMove : MonoBehaviour
{
    public VideoPlayer vid;

    private void Awake()
    {
        vid = GetComponent<VideoPlayer>();
    }
    void Start() 
    { 
        vid.loopPointReached += CheckOver; 
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene("Sans");
    }

    
}
