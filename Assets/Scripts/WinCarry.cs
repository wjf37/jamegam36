using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCarry : MonoBehaviour
{
    public static WinCarry Instance;
    public bool gameWon;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetBool(bool _gameWon)
    {
        gameWon = _gameWon;
    }

}
