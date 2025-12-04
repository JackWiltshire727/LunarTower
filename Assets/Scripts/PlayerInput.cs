using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float horizontalInput;
    public bool jumpPressed;
    public bool disableHorizontal = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (!disableHorizontal){
            horizontalInput = Input.GetAxis("Horizontal");
        }
        jumpPressed = Input.GetKeyDown(KeyCode.Space);
    }
}
