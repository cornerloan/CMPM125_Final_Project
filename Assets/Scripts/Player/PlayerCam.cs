using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public Transform orientation;
    public InputManager inputManager;  // Reference to InputManager

    private float xRotation = 0f;
    private float yRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Get mouse input from InputManager
        Vector2 mouseInput = inputManager.GetMouseInput();
        yRotation += mouseInput.x;
        xRotation -= mouseInput.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotation to camera and player orientation
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
