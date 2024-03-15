using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public bool gameWon = false;
    public float spawnRate = 4f;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] List<GameObject> spawnLocs;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject player;
    [SerializeField] GameObject candleStandard;
    [SerializeField] GameObject candleCalm;
    [SerializeField] GameObject candleCrazy;
    [SerializeField] GameObject aimWeapon;
    [SerializeField] GameObject failScreen;
    [SerializeField] GameObject winScreen;
    [SerializeField] Animator playerAnim;
    private bool paused = false;
    private PlayerMovement pMScript;
    private float tScale;
    private AimWeapon aWScript;
    private string gOText;

    

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        pMScript = player.GetComponent<PlayerMovement>();
        aWScript = aimWeapon.GetComponent<AimWeapon>();
        tScale = 1f;
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
        if(Input.GetKeyDown(KeyCode.C)){
            CalmMode();
        }
        if(Input.GetKeyDown(KeyCode.V)){
            CrazyMode();
        }
        if(Input.GetKeyDown(KeyCode.B)){
            StandardMode();
        }    
    }

    public void GameOver(){
        isGameActive = false;
        Time.timeScale = 0;
        failScreen.SetActive(true);
        //SceneManager.LoadScene(1);
    }
    
    public void Win(){
        isGameActive = false;
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, enemyPrefabs.Count);

            if (isGameActive)
            {
                Instantiate(enemyPrefabs[index], spawnLocs[Random.Range(0,spawnLocs.Count)].transform.position, enemyPrefabs[index].transform.rotation);
            }
            
        }
    }

    void CrazyMode(){
        candleCrazy.SetActive(true);
        candleStandard.SetActive(false);
        candleCalm.SetActive(false);
        tScale = 2f;
        Time.timeScale = tScale;
        pMScript.speed = 3;
        aWScript.cooldownDuration = 0.5f;
        aWScript.counterDuration = 1f;
        aWScript.baseDamage = 40;
        playerAnim.SetFloat("animSpeed",1f);
    }

    void CalmMode(){
        candleCrazy.SetActive(false);
        candleStandard.SetActive(false);
        candleCalm.SetActive(true);
        tScale = 0.5f;
        Time.timeScale = tScale;
        pMScript.speed = 6;
        aWScript.cooldownDuration = 0.15f;
        aWScript.counterDuration = 0.5f;
        aWScript.baseDamage = 80;
        playerAnim.SetFloat("animSpeed",2f);
    }

    void StandardMode(){
        candleCrazy.SetActive(false);
        candleStandard.SetActive(true);
        candleCalm.SetActive(false);
        tScale = 1f;
        Time.timeScale = tScale;
        pMScript.speed = 3;
        aWScript.cooldownDuration = 0.5f;
        aWScript.counterDuration = 1f;
        aWScript.baseDamage = 40;
        playerAnim.SetFloat("animSpeed",1f);
    }

    public void WinButton(){
        SceneManager.LoadScene(3);
        Time.timeScale = 1f;
    }

    public void LoseButton()
    {
        SceneManager.LoadScene(4);
        Time.timeScale= 1f;
    }

    public void ExitGame(){
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
        Application.Quit();
    #endif   
    }
}
