using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float maxSpeed = 1f;
    public float minSpeed = 0.5f;
    public float slowingDistance = 6f;
    private Rigidbody2D enemyRb;
    private Rigidbody2D playerRb;
    private Transform sprite;
    private EnemyCombat enemyCombat;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        sprite = transform.Find("Enemy Sprite");
        enemyCombat = GetComponent<EnemyCombat>();

        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        agent.destination = playerRb.position;
        if (!enemyCombat.attackStance){
            ChasePlayer();
        }
        else
        {
            enemyRb.velocity = Vector3.zero;
            agent.velocity = Vector3.zero;
        }
    }

    void ChasePlayer(){
        float distanceToPlayer = Vector2.Distance(enemyRb.position, playerRb.position);
        Vector2 direction = (playerRb.position - enemyRb.position).normalized;

        /*if (distanceToPlayer < slowingDistance){
            float speed = Mathf.Lerp(minSpeed, maxSpeed, Mathf.Pow(distanceToPlayer / slowingDistance, 2));
            //enemyRb.velocity = agent.velocity*speed*0.1f;
            //agent.velocity *= speed*0.13f;
            FlipSprite(direction.x);
        }
        else{   */
        FlipSprite(direction.x);
        //}
        //enemy speed drops off as they get closer to the player
        //once the enemy gets into slowing distance they boost forward a little bit.
        
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
