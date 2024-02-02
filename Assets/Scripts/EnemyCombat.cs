using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int maxHealth = 100;
    public float attackPause = 0.5f;
    public float attackCooldown = 2f;
    public bool attackStance = false;
    public bool deflected;
    private Transform attackRange;
    private int currentHealth; 
    private Rigidbody2D enemyRb;
    private Rigidbody2D playerRb;
    private bool isAttacking = false;
    private bool inRange = false;
    private SpriteRenderer sprite;
    private SpriteRenderer attackSprite;
    private Animator animator;
    private Transform animations;
    private Transform spriteTransform;
    private AudioSource audioSource;
    private PlayerCombat playerCombat;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        enemyRb = GetComponent<Rigidbody2D>();
        attackRange = transform.Find("AttackRange");
        sprite = transform.Find("Enemy Sprite").GetComponent<SpriteRenderer>();
        spriteTransform = transform.Find("Enemy Sprite");
        attackSprite = transform.Find("AttackRange/Animations").GetComponent<SpriteRenderer>();
        attackSprite.enabled = false;
        animator = transform.Find("AttackRange/Animations").GetComponent<Animator>();
        animations = transform.Find("AttackRange/Animations");
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyPlayerRange();
        if(deflected == true){
            StartCoroutine(Deflected());
        }
    }
    public void TakeDamage(int damage){
        currentHealth -= damage;
        audioSource.PlayOneShot(audioSource.clip);
        StartCoroutine(Shake());
        if (currentHealth<0){
            Die();
        }
    }

    void Die(){
        Destroy(gameObject);
    }

    private void EnemyPlayerRange(){
        Vector2 direction = playerRb.position - (Vector2)transform.position;
        attackRange.up = -direction.normalized;
        FlipSprite(attackRange.transform.rotation.eulerAngles.z);
    }
    
    void OnTriggerEnter2D(){
        inRange = true;
        if (!isAttacking)
        {
            StartCoroutine(AttackSequence());
        }
    }

    void OnTriggerExit2D(){
        inRange = false;
    }

    IEnumerator AttackSequence(){
        isAttacking = true;
        //Enemy turns red
        sprite.material.color = new Color(0.8f,0,0,1);
        attackStance = true;
        
        attackSprite.enabled = true;
        enemyRb.constraints |= RigidbodyConstraints2D.FreezePosition;
        
        yield return new WaitForSeconds(attackPause);
        // Reset color and set attacking state
        if(deflected == true){
            enemyRb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
            isAttacking = false;
            attackStance = false;
            attackSprite.enabled = false;
            yield return new WaitForSeconds(attackCooldown);
            yield break;
        }
        sprite.material.color = Color.white;
        attackStance = false;

        animator.SetTrigger("enemyAttack");
        if (inRange){
            playerCombat.TakeDamage(40);
        }
        Debug.Log("Enemy is attacking!");
        enemyRb.constraints &= ~RigidbodyConstraints2D.FreezePosition;

        isAttacking = false;
        attackSprite.enabled = false;
        yield return new WaitForSeconds(attackCooldown);
    }

    void FlipSprite(float z){
        if (z > 180)
        {
            animations.localScale = new Vector3(1, -1, 1); // Flip vertically
        }
        else if (z < 180)
        {
            animations.localScale = new Vector3(1, 1, 1); // No scaling needed
        }
    }
    IEnumerator Shake(){
        Vector3 originalPosition = spriteTransform.position;
        float elapsed = 0f;

        while (elapsed < 0.2f)
        {
            float x = originalPosition.x + Random.Range(-0.1f, 0.1f) * 1;

            transform.position = new Vector3(x, originalPosition.y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // Reset to the original position after shaking
        spriteTransform.position = originalPosition;
    }

    IEnumerator Deflected(){
        sprite.material.color = Color.yellow;
        attackSprite.enabled = false;
        yield return new WaitForSeconds(1f);
        deflected = false;
        sprite.material.color = Color.white;
    }
}
