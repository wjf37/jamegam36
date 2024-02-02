using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimWeapon : MonoBehaviour
{
    public int baseDamage;
    public GameObject animationPlayer;
    public float counterDuration = 1f;
    public float cooldownDuration = 0.5f;
    private GameObject enemyToAttack;
    private EnemyCombat enemyScript;
    private bool enemyInRange;
    private bool counter;
    private bool cooldown;
    private AnimationPlayer aPScript;


    // Start is called before the first frame update
    void Start()
    {
        enemyInRange = false;
        //spriteRenderer = transform.Find("AnimationPlayer").GetComponent<SpriteRenderer>();
        //animator= transform.Find("AnimationPlayer").GetComponent<Animator>();
        counter = false;
        cooldown = false;
        aPScript = animationPlayer.GetComponent<AnimationPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && enemyInRange && enemyScript.attackStance){
            Deflect();
        }
        if (Input.GetMouseButtonDown(0) && enemyInRange && counter && !cooldown){
            Counter();
            counter = false;
            StartCoroutine(AttackCoolDown());
        }
        else if (Input.GetMouseButtonDown(0) && enemyInRange && !cooldown){
            Attack();
            StartCoroutine(AttackCoolDown());
        } 
        else if (Input.GetMouseButtonDown(0) & !cooldown){
            aPScript.ChooseAnim("basicAttack");
            StartCoroutine(AttackCoolDown());
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        enemyInRange = true;
        enemyToAttack = other.gameObject;
        enemyScript = enemyToAttack.GetComponent<EnemyCombat>();
    }

    void OnTriggerExit2D(Collider2D other){
        enemyInRange = false;
    }
    void Attack(){
        Debug.Log("Player Attack:" + enemyToAttack);
        enemyScript.TakeDamage(baseDamage);
        aPScript.ChooseAnim("basicAttack");
    }

    void Deflect(){
        StartCoroutine(SetCounterTrue(1f));
        if(enemyToAttack.CompareTag("Enemy")){
            aPScript.ChooseAnim("deflectSpear");
            enemyScript.deflected = true;
        }
        else if(enemyToAttack.CompareTag("Enemy2")){
            aPScript.ChooseAnim("deflectSword");
            enemyScript.deflected = true;
        }
    }
    void Counter(){
        enemyScript.TakeDamage(baseDamage+20);
        if(enemyToAttack.CompareTag("Enemy")){
            aPScript.ChooseAnim("counterSpear");
        }
        else if(enemyToAttack.CompareTag("Enemy2")){
            aPScript.ChooseAnim("counterSword");
        }
    }

    IEnumerator SetCounterTrue(float duration){
        counter = true;
        yield return new WaitForSeconds(duration);
        counter = false;
    }
    IEnumerator AttackCoolDown(){
        cooldown = true;
        yield return new WaitForSeconds(0.5f);
        cooldown = false;
    }
}
