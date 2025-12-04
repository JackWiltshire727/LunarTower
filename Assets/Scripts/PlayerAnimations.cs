using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public PlayerInput inputScript;
    public PlayerController controllerScript;
    public Animator animator;
    public SpriteRenderer sprite;
    public bool directionFlip = true;
    void Start()
    {
        controllerScript = GetComponent<PlayerController>();
        inputScript = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Set horizontal direction and walking/idle animations
        if (inputScript.horizontalInput < 0)
        {
            sprite.flipX = directionFlip;
            animator.SetFloat("Speed", 1);
            animator.SetBool("HoldHorizontal",true);
        }
        else if (inputScript.horizontalInput > 0)
        {
            sprite.flipX = !directionFlip;
            animator.SetFloat("Speed", 1);
            animator.SetBool("HoldHorizontal",true);
        }
        else
        {
            animator.SetFloat("Speed",0);
            animator.SetBool("HoldHorizontal",false);
        }

        //Ground check
        if (controllerScript.isGrounded == true)
        {
            animator.SetBool("IsGrounded",true);
        }
        else
        {
            animator.SetBool("IsGrounded",false);
        }

        if (controllerScript.hasJumped == true)
        {
            animator.SetBool("IsPreparingJump",true);
        }
        else
        {
            animator.SetBool("IsPreparingJump",false);
        }

        animator.SetFloat("VerticalSpeed", controllerScript.verticalSpeed);

        if (controllerScript.onWallLeft || controllerScript.onWallRight)
        {
            //animator.SetBool("OnWall", true);
            if (!controllerScript.isGrounded){
                directionFlip = false;
            }
            if (inputScript.horizontalInput < 0) {sprite.flipX = directionFlip;}
            else if (inputScript.horizontalInput > 0) {sprite.flipX = !directionFlip;}
            animator.SetBool("OnWall", true);
        }
        else
        {
            //animator.SetBool("OnWall",false);
            if (!controllerScript.isGrounded){
                directionFlip = true;
            }
            if (inputScript.horizontalInput < 0) {sprite.flipX = directionFlip;}
            else if (inputScript.horizontalInput > 0) {sprite.flipX = !directionFlip;}
            animator.SetBool("OnWall",false);
        }

        animator.SetBool("LeaveWall",!((controllerScript.onWallLeft || controllerScript.onWallRight)&&(controllerScript.inputScript.horizontalInput!=0)));

        if (controllerScript.doubleJumpUsed && (!controllerScript.doubleOccured))
        {
            animator.SetBool("DoubleJumpPressed",true);
            controllerScript.doubleOccured = true;
        }
        else
        {
            animator.SetBool("DoubleJumpPressed",false);
        }
    }
}
