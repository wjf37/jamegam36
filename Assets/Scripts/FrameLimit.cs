using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameLimit : MonoBehaviour
{
    private int target = 60;
    private FrameLimit instance;
     
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;

    }
    
    void Update()
    {
        if(Application.targetFrameRate != target)
        {   
            Application.targetFrameRate = target;
        }
    }
}
