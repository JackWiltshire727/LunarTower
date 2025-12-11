using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{

    public PlayerInput inputScript;
    public PlayerMove moveScript;
    public PlayerAnimations animationScript;
    public bool isGrounded = true;
    public Rigidbody2D rb;
    public Transform GroundCheck;
    public Transform rightWallCheck;
    public Transform leftWallCheck;
    public LayerMask groundLayer;
    public bool hasJumped = false;
    public float verticalSpeed = 0.0f;
    bool[] abilities = { true, false, false, false};
    public bool onWallLeft = false;
    public bool onWallRight = false;
    public float wallCoyoteTime = 0.2f;
    private float wallCoyoteCounter = 0f;
    public int health = 3;
    public Vector2 checkpoint = new Vector2(0,2f);
    public bool doubleJumpUsed = false;
    public bool doubleOccured = false;
    public HeartUI heartUI;
    public GameOverManager gameManager;
    public int lastDirection = 1;
    public bool isDashing = false;
    public float dashTime = 0.3f;
    public float dashTimer = 0.3f;
    public bool dashUsed = false;
    private float dashCooldownTimer = 0.8f;
    private float dashCooldown = 0.8f;
    public AbilityPopupController popup;
    public GameObject checkpointDouble;
    public GameObject checkpointDash;
    public AudioSource audioSource;
    public AudioClip takeDamage;
    public AudioClip jump;
    public AudioClip dash;
    public AudioClip abilityUnlocked;


    void Start()
    {
        inputScript = GetComponent<PlayerInput>();
        moveScript = GetComponent<PlayerMove>();
        animationScript = GetComponent<PlayerAnimations>();
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
            dashUsed = false;
        }

        //Wall cling ability
        if (abilities[0] == true)
        {
            //Check if clinging to wall
            onWallRight = Physics2D.OverlapCircle(rightWallCheck.position, 0.05f, groundLayer);
            onWallLeft = Physics2D.OverlapCircle(leftWallCheck.position, 0.05f, groundLayer);

            //Freeze if clinging to wall
            if (onWallRight && (inputScript.horizontalInput > 0))
            {
                moveScript.WallCling(rb);
                wallCoyoteCounter = wallCoyoteTime;

                //Reset double jump
                doubleJumpUsed = false;
                doubleOccured = false;
                dashUsed = false;
            }
            else if (onWallLeft && (inputScript.horizontalInput < 0))
            {
                moveScript.WallCling(rb);
                wallCoyoteCounter = wallCoyoteTime;

                //Reset double jump
                doubleJumpUsed = false;
                doubleOccured = false;
                dashUsed = false;
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
                audioSource.PlayOneShot(jump);

                //Reset double jump
                doubleJumpUsed = false;
                doubleOccured = false;
                dashUsed = false;
            }
        }

        //Get vertical speed
        verticalSpeed = rb.linearVelocity.y;
        
        if(!isDashing){
            //HorizontalMovement
            if (!inputScript.disableHorizontal){
                moveScript.ApplyHorizontal(rb,inputScript.horizontalInput);
                if (inputScript.horizontalInput < 0)
                {
                    lastDirection = -1;
                }
                else if (inputScript.horizontalInput > 0)
                {
                    lastDirection = 1;
                }
            }
        }
        else
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
                dashTimer = dashTime;
            }
        }

        //Apply jump
        if (inputScript.jumpPressed && isGrounded)
        {
            moveScript.ApplyJumpPress(rb);
            hasJumped = true;
            audioSource.PlayOneShot(jump);
        }

        //Apply double jump
        if (inputScript.jumpPressed && (!isGrounded) && (!doubleJumpUsed) && abilities[1] && (!onWallLeft) && (!onWallRight))
        {
            doubleJumpUsed = true;
            moveScript.ApplyDoubleJumpPress(rb);
            audioSource.PlayOneShot(jump);
        }

        //Stronger gravity when falling
        if (rb.linearVelocity.y < 0 && !isDashing)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (2.0f - 1) * Time.deltaTime;
        }

        //Apply dash
        if (inputScript.dashPressed && abilities[2] && !isDashing && !dashUsed && (dashCooldownTimer <= 0) && !isGrounded)
        {
            audioSource.PlayOneShot(dash,0.3f);
            moveScript.Dash(rb, lastDirection);
            isDashing = true;
            dashUsed = true;
            dashCooldownTimer = dashCooldown;
        }

        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
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
            audioSource.PlayOneShot(takeDamage);
            animationScript.directionFlip = true;
            health--;
            heartUI.UpdateHearts(health);

            if (health > 0){
                transform.position = checkpoint;
                rb.linearVelocity = new Vector2(0,0);
            }
            else
            {
                gameManager.GameOver();
            }
        }

        if (other.CompareTag("DoubleJump"))
        {
            if (abilities[1] == false)
            {
                audioSource.PlayOneShot(abilityUnlocked);
                popup.ShowAbility("Your connection to the moon deepens.\n\nYou have learned the spell Moonstep.\n\nPress Space while in the air to leap higher than before.");
                abilities[1] = true;
                health = 3;
                heartUI.UpdateHearts(health);
                checkpoint = new Vector3(-6.6f,39.9f);
                checkpointDouble.SetActive(false);
            }
        }

        if (other.CompareTag("Dash"))
        {
            if (abilities[2] == false)
            {
                audioSource.PlayOneShot(abilityUnlocked);
                popup.ShowAbility("Your connection to the moon deepens.\n\nYou have learned the spell Lunar Dash.\n\nPress Shift while in the air to dash forward.");
                abilities[1] = true;
                abilities[2] = true;
                health = 3;
                heartUI.UpdateHearts(health);
                checkpoint = new Vector3(-5.32f,101.79f);
                checkpointDash.SetActive(false);
            }
        }

        if (other.CompareTag("Final"))
        {
            SceneManager.LoadScene("EndingScene");
        }
    }
}
