using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public PlayerInput inputScript;
    public PlayerController controllerScript;
    public Animator animator;
    public SpriteRenderer sprite;
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
            sprite.flipX = true;
            animator.SetFloat("Speed", 1);
        }
        else if (inputScript.horizontalInput > 0)
        {
            sprite.flipX = false;
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed",0);
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
    }
}
