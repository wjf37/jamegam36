using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int maxHealth = 100;
    public GameObject mouseIndicator;
    public LayerMask enemyLayer;
    public List<AnimationClip> attackAnimations;
    public AudioSource audioSource;
    public HealthBar healthBar;
    private Vector2 mousePos;
    private int currentHealth;
    private Transform animations;
    private Transform spriteTransform;
    public AudioClip attacked;
    private GameManager gameManager;
    private SpriteRenderer pSprite;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animations = transform.Find("Combat Direction/AnimationPlayer");
        spriteTransform = transform.Find("Sprite");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pSprite = spriteTransform.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CombatFollowMouse();
    }
    private void CombatFollowMouse(){
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - (Vector2)transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        mouseIndicator.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        FlipSprite(mouseIndicator.transform.rotation.eulerAngles.z);

    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.UpdateHealth(currentHealth);
        audioSource.PlayOneShot(attacked);
        StartCoroutine(Shake());
        if (currentHealth<0){
            Die();
        }
    }

    void Die(){
        gameManager.GameOver();
    }

   void FlipSprite(float z)
    {
        if (z < 180)
        {
            animations.localScale = new Vector3(1, -1, 1); // Flip vertically
        }
        else if (z > 180)
        {
            animations.localScale = new Vector3(1, 1, 1); // No scaling needed
        }
    }
    IEnumerator Shake(){
        Vector3 originalPosition = spriteTransform.position;
        float elapsed = 0f;
        pSprite.material.color = Color.red;
        while (elapsed < 0.2f)
        {
            float x = originalPosition.x + Random.Range(-0.1f, 0.1f) * 1;

            transform.position = new Vector3(x, originalPosition.y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        pSprite.material.color = Color.white;
        // Reset to the original position after shaking
        spriteTransform.position = originalPosition;
    }
}
