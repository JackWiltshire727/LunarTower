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

    public void ApplyDoubleJumpPress(Rigidbody2D rb)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPressForce);
    }

    public void ApplyHorizontal(Rigidbody2D rb, float horizontalInput)
    {
        rb.linearVelocity = new Vector2(horizontalInput * speed,rb.linearVelocity.y);
    }

    public void WallCling(Rigidbody2D rb)
    {
        rb.linearVelocity = new Vector2(0,0);
    }

    public void wallJump(Rigidbody2D rb, int direction)
    {
        rb.linearVelocity = new Vector2(speed*direction*0.7f,jumpPressForce*1.1f);
    }
}
