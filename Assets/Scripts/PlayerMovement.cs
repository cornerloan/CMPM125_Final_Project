using UnityEngine;

public class PlayerController : MonoBehaviour
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
        // Check if the crouch key is pressed or if there’s an obstruction above
        if (grounded && (inputManager.IsCrouchPressed() || !CanStandUp()))
        {
            // Remain in crouch state if crouch key is pressed or there's no space to stand up
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
        }
        // Transition to sprinting state only if grounded, crouch key is released, and there's space to stand
        else if (grounded && inputManager.IsSprintPressed() && CanStandUp())
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
            AdjustPlayerScale();
        }
        // Walking state: transition if grounded, crouch key is released, and enough space to stand
        else if (grounded && CanStandUp())
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
            AdjustPlayerScale();
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
        float standUpCheckDistance = startYScale - crouchYScale + 0.1f;
        Vector3 checkPosition = transform.position + Vector3.up * crouchYScale;
        return !Physics.Raycast(checkPosition, Vector3.up, standUpCheckDistance, Ground);
    }

    private void AdjustPlayerScale()
    {
        // Smoothly transition to standing if possible
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
