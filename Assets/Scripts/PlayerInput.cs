using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float horizontalInput;
    public bool jumpPressed;
    public bool jumpHeld;

    void Start()
    {
        
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
        jumpHeld = Input.GetKey(KeyCode.Space);
    }
}
