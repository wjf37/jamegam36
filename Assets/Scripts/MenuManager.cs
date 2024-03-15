using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject howToPlayScreen1;
    [SerializeField] GameObject howToPlayScreen2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        SceneManager.LoadScene(1);
    }
    
    public void ExitGame(){
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif   
    }

    public void HowToPlay()
    {
        titleScreen.SetActive(false);
        howToPlayScreen1.SetActive(true);
    }

    public void NextPage()
    {
        howToPlayScreen1.SetActive(false);
        howToPlayScreen2.SetActive(true);
    }
    
    public void PrevPage()
    {
        howToPlayScreen2.SetActive(false);
        howToPlayScreen1.SetActive(true);
    }
    
    public void BackToMenu()
    {
        titleScreen.SetActive(true);
        howToPlayScreen2.SetActive(false);        
    }
}
