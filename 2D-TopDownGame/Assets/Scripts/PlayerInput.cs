using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{   
    Vector2 movement;
    public Rigidbody2D rb;
    public float moveSpeed = 3f;

    void Start()
    {
        
    }


    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.deltaTime * movement);
    }
}
