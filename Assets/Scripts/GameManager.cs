using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public bool gameOverFlag = false;
    public List<GameObject> enemyPrefabs;
    public List<GameObject> spawnLocs;
    public GameObject pauseScreen;
    public float spawnRate = 5f;
    public GameObject player;
    public GameObject candleStandard;
    public GameObject candleCalm;
    public GameObject candleCrazy;
    public GameObject aimWeapon;
    public GameObject failScreen;
    public GameObject winScreen;
    public Animator playerAnim;
    private bool paused = false;
    private PlayerCombat pCScript;
    private PlayerMovement pMScript;
    private float tScale;
    private AimWeapon aWScript;
    private string gOText;

    

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        pCScript = player.GetComponent<PlayerCombat>();
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
    
    public  void Win(){
        isGameActive = false;
        Time.timeScale = 0;
        winScreen.SetActive(true);
        //SceneManager.LoadScene(1);
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
        aWScript.baseDamage = 60;
        playerAnim.SetFloat("animSpeed",1f);
    }

    void CalmMode(){
        candleCrazy.SetActive(false);
        candleStandard.SetActive(false);
        candleCalm.SetActive(true);
        tScale = 0.5f;
        Time.timeScale = tScale;
        pMScript.speed = 6;
        aWScript.cooldownDuration = 0.25f;
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

    public void B2Menu(){
        SceneManager.LoadScene(0);
    }
}
