using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce = 100000f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private LayerMask groundObjects;
    [SerializeField] private float checkRadius;
    [SerializeField] private int maxJumpCount;


    private bool isJumping = false;
    private bool facingRight = true;
    private bool isGrounded;

    private int jumpCount;


    private float moveDirection;
    private Rigidbody2D _rb;

    private void Awake()
    {
       _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        jumpCount = maxJumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();

        Animate();

        

    }
    private void FixedUpdate()
    {
        Move();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
        if (isGrounded)
        {
            jumpCount= maxJumpCount;
        }

    }

    private void Move()
    {
        _rb.velocity = new Vector2(moveDirection * moveSpeed, _rb.velocity.y);
        if (isJumping && jumpCount > 0)
        {
            _rb.AddForce(new Vector2(0f, jumpForce));
            jumpCount--;
        }
        isJumping = false;
    }

    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
    }

    private void FlipCharacter()
    {
        facingRight= !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
