using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float jumpPressForce = 5.0f;
    private float speed = 6.0f;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ApplyJumpPress(Rigidbody2D rb)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPressForce);
    }

    public void ApplyHorizontal(Rigidbody2D rb, float horizontalInput)
    {
        rb.linearVelocity = new Vector2(horizontalInput * speed,rb.linearVelocity.y);
    }
}
