using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float horizontalInput;
    public bool jumpPressed;
    public bool disableHorizontal = false;
    public bool dashPressed;

    void Start()
    {
        
    }

    void Update()
    {
        if (!disableHorizontal){
            horizontalInput = Input.GetAxis("Horizontal");
        }
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
        dashPressed = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);;
    }
}
