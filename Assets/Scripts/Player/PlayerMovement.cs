using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public float groundDrag = 5f;
    public float airDrag = 1f; // Lesser drag when in the air

    [Header("Jumping")]
    public float jumpForce = 10f;
    public float moveJumpHeightMultiplier = 0.8f;
    public float maxHorizontalJumpBoost = 2f;
    public float jumpCoolDown = 0.2f;
    public float airMultiplier = 0.5f;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    public float groundCheckRadius = 0.4f;
    public Transform groundCheck;
    public float maxSlopeAngle = 45f;
    public LayerMask Ground;

    public Transform orientation;
    public InputManager inputManager;

    private bool canJump;
    private bool grounded;
    private bool onSlope;
    private Vector3 slopeMoveDirection;
    private Vector3 moveDirection;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canJump = true;
    }

    private void Update()
    {
        // Check if grounded using groundCheck position
        grounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, Ground);
        onSlope = IsOnSlope();

        PlayerInput();
        SpeedControl();

        // Apply ground drag or air drag based on whether the player is grounded
        rb.drag = grounded ? groundDrag : airDrag;
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerInput()
    {
        if (inputManager.IsJumpPressed() && canJump && grounded)
        {
            canJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCoolDown);
        }
    }

    private void PlayerMove()
    {
        Vector2 input = inputManager.GetMovementInput();
        moveDirection = orientation.forward * input.y + orientation.right * input.x;
        float currentSpeed = moveSpeed;

        // Increase speed if sprinting on the ground
        if (inputManager.IsSprintPressed() && grounded)
        {
            currentSpeed *= sprintMultiplier;
        }

        if (grounded)
        {
            if (onSlope)
            {
                // Move along the slope direction
                rb.AddForce(slopeMoveDirection * currentSpeed * 10f, ForceMode.Force);
            }
            else if (moveDirection.magnitude > 0.1f)
            {
                rb.AddForce(moveDirection.normalized * currentSpeed * 10f, ForceMode.Force);
            }
        }
        else
        {
            // Apply reduced force while in the air
            rb.AddForce(moveDirection.normalized * currentSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        float maxSpeed = inputManager.IsSprintPressed() && grounded ? moveSpeed * sprintMultiplier : moveSpeed;

        // Limit player speed to max speed
        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // Reset vertical velocity before jumping to ensure consistent jump height
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        bool isMoving = moveDirection.magnitude > 0.1f;
        float appliedJumpForce = isMoving ? jumpForce * moveJumpHeightMultiplier : jumpForce;

        // Apply upward force for the jump
        rb.AddForce(Vector3.up * appliedJumpForce, ForceMode.Impulse);

        // Apply additional horizontal force based on current move direction and speed
        if (isMoving)
        {
            Vector3 horizontalBoost = moveDirection.normalized * Mathf.Min(moveSpeed * sprintMultiplier, maxHorizontalJumpBoost);
            rb.AddForce(horizontalBoost, ForceMode.Impulse);
        }
    }

    private bool IsOnSlope()
    {
        if (Physics.Raycast(groundCheck.position, Vector3.down, out RaycastHit hit, playerHeight / 2 + 0.5f, Ground))
        {
            float slopeAngle = Vector3.Angle(Vector3.up, hit.normal);
            slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, hit.normal).normalized;
            return slopeAngle < maxSlopeAngle && slopeAngle != 0;
        }
        return false;
    }

    private void ResetJump()
    {
        canJump = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
