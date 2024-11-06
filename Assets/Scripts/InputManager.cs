using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Key Bindings")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Mouse Sensitivity")]
    public float sensitivityX = 100f;
    public float sensitivityY = 100f;

    public Vector2 GetMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        return new Vector2(horizontal, vertical);
    }

    public bool IsJumpPressed()
    {
        return Input.GetKey(jumpKey);
    }

    public bool IsSprintPressed()
    {
        return Input.GetKey(sprintKey);
    }

    public bool IsCrouchPressed()
    {
        return Input.GetKey(crouchKey);
    }

    public bool IsCrouchUp()
    {
        return Input.GetKeyUp(crouchKey);
    }

    public Vector2 GetMouseInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivityY;
        return new Vector2(mouseX, mouseY);
    }
}

// Mouse input for looking around

