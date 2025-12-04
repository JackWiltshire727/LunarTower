using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{

    public PlayerInput inputScript;
    public PlayerMove moveScript;
    public bool isGrounded = true;
    public Rigidbody2D rb;
    public Transform GroundCheck;
    public Transform rightWallCheck;
    public Transform leftWallCheck;
    public LayerMask groundLayer;
    public bool hasJumped = false;
    public float verticalSpeed = 0.0f;
    bool[] abilities = { true, true, false, false};
    public bool onWallLeft = false;
    public bool onWallRight = false;
    public float wallCoyoteTime = 0.2f;
    private float wallCoyoteCounter = 0f;
    public int health = 3;
    public Vector2 checkpoint = new Vector2(0,-0.2f);
    public bool doubleJumpUsed = false;
    public bool doubleOccured = false;


    void Start()
    {
        inputScript = GetComponent<PlayerInput>();
        moveScript = GetComponent<PlayerMove>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, groundLayer);
        if (isGrounded)
        {
            hasJumped = false;
            doubleJumpUsed = false;
            doubleOccured = false;
        }

        //Wall cling ability
        if (abilities[0] == true)
        {
            //Check if clinging to wall
            onWallRight = Physics2D.OverlapCircle(rightWallCheck.position, 0.1f, groundLayer);
            onWallLeft = Physics2D.OverlapCircle(leftWallCheck.position, 0.1f, groundLayer);

            //Freeze if clinging to wall
            if (onWallRight && (inputScript.horizontalInput > 0))
            {
                moveScript.WallCling(rb);
                wallCoyoteCounter = wallCoyoteTime;

                //Reset double jump
                doubleJumpUsed = false;
                doubleOccured = false;
            }
            else if (onWallLeft && (inputScript.horizontalInput < 0))
            {
                moveScript.WallCling(rb);
                wallCoyoteCounter = wallCoyoteTime;

                //Reset double jump
                doubleJumpUsed = false;
                doubleOccured = false;
            }

            //Wall jump
            if (inputScript.jumpPressed && (onWallLeft||onWallRight) && (wallCoyoteCounter>0) && (inputScript.horizontalInput!=0))
            {
                int direction = 0;
                if (onWallRight) {direction = -1;}
                if (onWallLeft) {direction = 1;}
                StartCoroutine(DisableHorizontalCoroutine(0.2f));
                inputScript.horizontalInput = 0;
                moveScript.wallJump(rb, direction);
            }
        }

        //Get vertical speed
        verticalSpeed = rb.linearVelocity.y;
        

        //HorizontalMovement
        if (!inputScript.disableHorizontal){
            moveScript.ApplyHorizontal(rb,inputScript.horizontalInput);
        }

        //Apply jump
        if (inputScript.jumpPressed && isGrounded)
        {
            moveScript.ApplyJumpPress(rb);
            hasJumped = true;
        }

        //Apply double jump
        if (inputScript.jumpPressed && (!isGrounded) && (!doubleJumpUsed) && abilities[1])
        {
            doubleJumpUsed = true;
            moveScript.ApplyDoubleJumpPress(rb);
        }

    //Stronger gravity when falling
    if (rb.linearVelocity.y < 0)
    {
        rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (2.0f - 1) * Time.deltaTime;
    }
    }

    IEnumerator DisableHorizontalCoroutine(float duration)
    {
        //Temporarily disable horizontal input
        inputScript.disableHorizontal = true;
        yield return new WaitForSeconds(duration);
        inputScript.disableHorizontal = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Player takes damage event
        if (other.CompareTag("Damage"))
        {
            health--;
            transform.position = checkpoint;
            rb.linearVelocity = new Vector2(0,0);
        }
    }
}
