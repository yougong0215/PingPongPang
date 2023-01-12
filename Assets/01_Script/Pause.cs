using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] Image a;

    bool pause;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            a.gameObject.SetActive(true);
            pause = true;
            Time.timeScale = 0;
        }
    }

    public void PauseOff()
    {
        Time.timeScale = 1;
        pause = false;
        a.gameObject.SetActive(false);
    }

    public void StartMap()
    {
        SceneManager.LoadScene("StartScene");
    }
}
