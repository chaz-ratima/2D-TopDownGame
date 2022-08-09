using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{   
    Vector2 movement;
    public Rigidbody2D rb;
    public Animator animator;
    public float moveSpeed;
    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;
    private bool allowMovement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //movement = movement.normalized;
        if (!allowMovement) return;
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if(movement.x !=0 || movement.y !=0)
        {
            animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (!allowMovement) return;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else moveSpeed = walkSpeed;
        rb.MovePosition(rb.position + moveSpeed * Time.deltaTime * movement.normalized);
    }

    public void NoMoving()
    {
        allowMovement = false;
    }

    public void YesMoving()
    {
        allowMovement = true;
    }
}
