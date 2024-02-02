using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float maxSpeed = 7f;
    public float minSpeed = 0.5f;
    public float slowingDistance = 6f;
    private Rigidbody2D enemyRb;
    private Rigidbody2D playerRb;
    private Transform sprite;
    private EnemyCombat enemyCombat;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        sprite = transform.Find("Enemy Sprite");
        enemyCombat = GetComponent<EnemyCombat>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!enemyCombat.attackStance){
            ChasePlayer();
        }
    }

    void ChasePlayer(){
        float distanceToPlayer = Vector2.Distance(enemyRb.position, playerRb.position);
        Vector2 direction = (playerRb.position - enemyRb.position).normalized;

        if (distanceToPlayer < slowingDistance){
            float speed = Mathf.Lerp(minSpeed, maxSpeed, Mathf.Pow(distanceToPlayer / slowingDistance, 2));
            enemyRb.velocity = direction * speed;
            FlipSprite(direction.x);
        }
        else{
            enemyRb.velocity = direction * maxSpeed;
            FlipSprite(direction.x);
        }
        //enemy speed drops off as they get closer to the player
        
    }
    void FlipSprite(float x)
    {
        if (x > 0)
        {
            sprite.localScale = new Vector3(1, 1, 1); // No scaling needed
        }
        else if (x < 0)
        {
            sprite.localScale = new Vector3(-1, 1, 1); // Flip horizontally
        }
    }
}
