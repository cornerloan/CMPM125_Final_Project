using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float crouchSpeed = 1.5f;
    public float groundDrag = 5f;
    public float airDrag = 1f;

    [Header("Jumping")]
    public float jumpForce = 10f;
    public float jumpCoolDown = 0.2f;
    public float airMultiplier = 0.5f;

    [Header("Crouching")]
    public float crouchYScale = 0.5f;
    private float startYScale;

    [Header("Ground Check")]
    public float groundCheckRadius = 0.4f;
    public Transform groundCheck;
    public LayerMask Ground;

    public Transform orientation;
    public InputManager inputManager;

    private bool canJump;
    private bool grounded;
    private Vector3 moveDirection;
    private Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air,
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        canJump = true;
        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, Ground);

        PlayerInput();
        SpeedControl();
        StateHandler();

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
            Jump();
            canJump = false;
            Invoke(nameof(ResetJump), jumpCoolDown);
        }
    }

    private void StateHandler()
    {
        // Check if there's enough space to stand up
        bool canStand = CanStandUp();

        // Crouch state: stay crouched if crouch key is pressed or if there's an obstruction above
        if (grounded && (inputManager.IsCrouchPressed() || !canStand))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
        }
        // Transition to sprinting or walking state if crouch key is released and there's space to stand
        else if (grounded && canStand)
        {
            if (inputManager.IsSprintPressed())
            {
                state = MovementState.sprinting;
                moveSpeed = sprintSpeed;
            }
            else
            {
                state = MovementState.walking;
                moveSpeed = walkSpeed;
            }

            AdjustPlayerScale(); // Smoothly transition back to normal scale
        }
        // Air state: when the player is not grounded
        else
        {
            state = MovementState.air;
        }
    }

    private void PlayerMove()
    {
        Vector2 input = inputManager.GetMovementInput();
        moveDirection = orientation.forward * input.y + orientation.right * input.x;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private bool CanStandUp()
    {
        // Calculate the standing check distance
        float standUpCheckDistance = startYScale - crouchYScale + 0.1f; // Adding a small buffer for safety

        // Cast a ray from the player's position upwards
        Vector3 checkPosition = transform.position + Vector3.up * crouchYScale;
        return !Physics.Raycast(checkPosition, Vector3.up, standUpCheckDistance, Ground);
    }

    private void AdjustPlayerScale()
    {
        // Smoothly transition back to original height if allowed
        transform.localScale = Vector3.Lerp(transform.localScale,
            new Vector3(transform.localScale.x, startYScale, transform.localScale.z), Time.deltaTime * 10f);
    }

    private void ResetJump()
    {
        canJump = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.color = Color.blue;
        Vector3 checkPosition = transform.position + Vector3.up * crouchYScale;
        float standUpCheckDistance = startYScale - crouchYScale + 0.1f;
        Gizmos.DrawLine(checkPosition, checkPosition + Vector3.up * standUpCheckDistance);
    }
}
