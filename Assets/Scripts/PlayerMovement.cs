using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    InputAction moveAction;
    InputAction jumpAction;

    SpriteRenderer sr;
    Animator animator;

    private Rigidbody2D rb;
    private float mvX;
    private float mvY;

    private float moveSpeed;
    public float jumpHeight;

    public bool jumpState;
    public int moveDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        moveSpeed = 10.0f;
        jumpHeight = 23.0f;

        jumpState = false;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        jumpAction = InputSystem.actions.FindAction("Jump");
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Fixed Update is called a fixed number of times per second
    void FixedUpdate() {

        if ( jumpAction.WasReleasedThisFrame() ) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        if ( moveAction.WasReleasedThisFrame() ) {
            
        }

        if ( rb.linearVelocity.y > -0.1 && rb.linearVelocity.y < 0.1 ) {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
        }

        if ( rb.linearVelocity.y < -0.1 ) {
            animator.SetBool("Falling", true);
        }
        animator.SetFloat("SpeedHoriz", Mathf.Abs(rb.linearVelocity.x));


        if ( mvX == 0.0f ) {
            rb.linearVelocity = new Vector2(0.9f * rb.linearVelocity.x, rb.linearVelocity.y);
            
        }
        else {
            rb.linearVelocity = new Vector2(mvX * moveSpeed, rb.linearVelocity.y);
        }
    }

    void OnMove(InputValue movementValue) {
        Vector2 movementVec = movementValue.Get<Vector2>();
        mvX = movementVec.x;
        mvY = movementVec.y;

        if (mvX > 0) {
            sr.flipX = false;
        } else if ( mvX < 0 ) {
            sr.flipX = true;
        }
    }

    void OnJump(InputValue movementValue) {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
        
        animator.SetBool("Jumping", true);
    }
}