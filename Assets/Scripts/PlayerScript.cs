using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    [SerializeField] private int jumpVelocity;
    [SerializeField] private LayerMask PlatformLayerMask;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private SpriteRenderer sr;
    private bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        HandleJump();
    }

    private void FixedUpdate()
    {
        HandleSidewaysMovement();

    }

    private void HandleSidewaysMovement()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (Input.GetKey(KeyCode.A))
        {
            HandleLeftAnimation();
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                HandleRightAnimation();
                rb.velocity = new Vector2(+moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    private void HandleLeftAnimation()
    {
        if (facingRight)
        {
            facingRight = !facingRight;
            sr.flipX = true;
        }
    }

    private void HandleRightAnimation()
    {
        if (!facingRight)
        {
            facingRight = !facingRight;
            sr.flipX = false;
        }
    }

    private void HandleJump()
    {
        if (IsGrounded() && Input.GetKey(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpVelocity;
        }
    }

    private bool IsGrounded()
    {
        float extraHeightText = .1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, extraHeightText, PlatformLayerMask);
        return raycastHit.collider != null;
    }

}
