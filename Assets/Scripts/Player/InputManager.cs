using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Key Bindings")]
    public KeyCode jumpKey = KeyCode.Space;  // Key for jumping
    public KeyCode sprintKey = KeyCode.LeftShift; // Key for sprinting

    [Header("Mouse Sensitivity")]
    public float sensitivityX = 100f;
    public float sensitivityY = 100f;

    // Movement input (WASD / Arrow Keys)
    public Vector2 GetMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        return new Vector2(horizontal, vertical);
    }

    // Jump input
    public bool IsJumpPressed()
    {
        return Input.GetKey(jumpKey);
    }

    // Sprint input 
    public bool IsSprintPressed()
    {
        return Input.GetKey(sprintKey);
    }

    // Mouse input for looking around
    public Vector2 GetMouseInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivityY * Time.deltaTime;
        return new Vector2(mouseX, mouseY);
    }
}
