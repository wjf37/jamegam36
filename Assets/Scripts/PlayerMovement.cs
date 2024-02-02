using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3;
    public GameObject interactionBox;
    private Rigidbody2D rb;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();    
    }

    void Update(){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement);
        //+x = z 90 -x = z -90 +y = z 180 -y = z 0
        if (movement != Vector2.zero){
            FaceDirection();
        }
    }

    private void FaceDirection(){
        float angle = Mathf.Atan2(movement.x, -movement.y) * Mathf.Rad2Deg;
        interactionBox.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
