using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerInput inputScript;
    public PlayerMove moveScript;
    public bool isGrounded = true;
    public Rigidbody2D rb;
    public Transform GroundCheck;
    public LayerMask groundLayer;

    void Start()
    {
        inputScript = GetComponent<PlayerInput>();
        moveScript = GetComponent<PlayerMove>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //HorizontalMovement
        moveScript.ApplyHorizontal(rb,inputScript.horizontalInput);


        //Ground check
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, groundLayer);
    }
}
