using System;
using System.Collections;
using System.Collections.Generic;   
using UnityEngine;
using UnityEngine.SceneManagement;

public class CandleScript : MonoBehaviour
{
    public bool colliding = false;
    public GameObject tooltipManager;
    private TooltipManager tooltipScript;
    // Start is called before the first frame update
    void Start()
    {
        tooltipScript = tooltipManager.GetComponent<TooltipManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        tooltipScript.ShowTooltip(colliding);
        if (colliding == true && Input.GetKeyDown(KeyCode.E)){
                SceneManager.LoadScene(2);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Trigger enter");
        if(other.CompareTag("InteractionHitbox")){
            colliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        Debug.Log("Trigger exit");
        if(other.CompareTag("InteractionHitbox")){
            colliding = false;
        }
    }

}
