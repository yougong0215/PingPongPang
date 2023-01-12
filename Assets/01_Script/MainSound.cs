using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSound : MonoBehaviour
{
    [SerializeField] AudioClip Audio;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            transform.GetChild(0).GetComponent<AudioSource>().PlayOneShot(Audio);
        }
        if(GameObject.Find("MapList"))
        {
            Destroy(this);
        }
    }
}
