using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Key Bindings")]
    public KeyCode jumpKey = KeyCode.Space;          // Key for jumping
    public KeyCode sprintKey = KeyCode.LeftShift;    // Key for sprinting
    public KeyCode crouchKey = KeyCode.LeftControl;  // Key for crouching

    [Header("Mouse Sensitivity")]
    public float sensitivityX = 100f; // Horizontal sensitivity
    public float sensitivityY = 100f; // Vertical sensitivity

    /// <summary>
    /// Gets the movement input for horizontal and vertical directions (e.g., WASD or arrow keys).
    /// Returns a Vector2 where x represents horizontal input and y represents vertical input.
    /// </summary>
    public Vector2 GetMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        return new Vector2(horizontal, vertical);
    }

    /// <summary>
    /// Checks if the jump key is currently pressed.
    /// </summary>
    public bool IsJumpPressed()
    {
        return Input.GetKey(jumpKey);
    }

    /// <summary>
    /// Checks if the sprint key is currently pressed.
    /// </summary>
    public bool IsSprintPressed()
    {
        return Input.GetKey(sprintKey);
    }

    /// <summary>
    /// Checks if the crouch key is currently held down.
    /// </summary>
    public bool IsCrouchPressed()
    {
        return Input.GetKey(crouchKey);
    }

    /// <summary>
    /// Checks if the crouch key was just released (useful for toggling crouch states).
    /// </summary>
    public bool IsCrouchUp()
    {
        return Input.GetKeyUp(crouchKey);
    }

    /// <summary>
    /// Gets mouse movement input for camera control.
    /// Adjusts sensitivity for smoother control.
    /// Returns a Vector2 where x represents horizontal movement and y represents vertical movement.
    /// </summary>
    public Vector2 GetMouseInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivityY;
        return new Vector2(mouseX, mouseY);
    }
}
