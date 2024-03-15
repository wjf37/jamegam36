using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private PauseManager instance;
     
    /*void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            paused = !paused;
            if(paused){
                Time.timeScale = 0f;
                pauseScreen.SetActive(true);
                isGameActive = false;
                AudioListener.pause = true;
            }
            else{
                Time.timeScale = 1f;
                pauseScreen.SetActive(false);
                isGameActive = true;
                AudioListener.pause = false;
            }
        }
    }*/
}
