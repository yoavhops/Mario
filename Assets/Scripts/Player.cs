using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private float jumpForce = 400;
    [SerializeField]
    private Sprite jumpSprite;



    private float H;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private new Rigidbody2D rigidbody2D;
    private CharcterGrounding charcterGrounding;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        charcterGrounding = GetComponent<CharcterGrounding>();
    }

    // Update is called once per frame
    void Update()
    {
        H = Input.GetAxisRaw("Horizontal");

        if (H != 0)
        {
            transform.position += new Vector3(H * speed * Time.deltaTime, 0);
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        if (H < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (Input.GetButtonDown("Fire1") && charcterGrounding.IsGrounded)
        {
            rigidbody2D.velocity = Vector3.zero;
            rigidbody2D.AddForce(Vector2.up * jumpForce);
            
        }

        if (!charcterGrounding.IsGrounded)
        {
            animator.enabled = false;
            spriteRenderer.sprite = jumpSprite;
        }
        else
        {
            animator.enabled = true;
        }
    }

    private void HandleAnimation(String animation)
    {
        if(animation == "Move")
        {
            animator.SetFloat("Speed", 1);
        }
        if(animation == "Idle")
        {
            animator.SetFloat("Speed", 0);
        }
    }
}
