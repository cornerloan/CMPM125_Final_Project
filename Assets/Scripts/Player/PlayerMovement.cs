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
    public LayerMask Ground; // Ensure this includes interactable layers

    public Transform orientation;
    public InputManager inputManager;

    private bool canJump;
    public bool grounded;
    public Vector3 moveDirection;
    private Rigidbody rb;

    private Transform originalParent; // To store the player's original parent
    private bool onMovingPlatform = false; // Track if the player is on a moving platform

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

        // Add Interactable layer to Ground mask programmatically
        Ground |= (1 << LayerMask.NameToLayer("Interactable"));
    }

    private void Update()
    {
        HandleInput();
        ControlDrag();
    }

    private void FixedUpdate()
    {
        UpdateGroundedStatus(); // Update grounded status with each physics update

        if (grounded || moveDirection.magnitude > 0.1f)
        {
            MovePlayer();
        }
    }

    // Improved ground check that includes platform parenting
    private void UpdateGroundedStatus()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, Ground);

        // Unparent the player if they’re no longer grounded
        if (!grounded && transform.parent != originalParent)
        {
            transform.parent = originalParent;
            onMovingPlatform = false; // Not on a platform anymore
        }

        // If the CheckSphere fails, use a raycast to further confirm grounding
        if (!grounded)
        {
            RaycastHit hit;
            float rayLength = groundCheckRadius + 0.1f;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength, Ground))
            {
                grounded = true;

                // If the player is standing on a moving platform
                if (hit.collider.GetComponent<ElevatorController>() != null)
                {
                    if (transform.parent != hit.collider.transform)
                    {
                        originalParent = transform.parent; // Save the current parent
                        transform.parent = hit.collider.transform; // Parent the player to the elevator
                    }
                    onMovingPlatform = true; // Indicate player is on a platform
                }
                else if (transform.parent != originalParent)
                {
                    // If stepping off the elevator, unparent and reset the state
                    transform.parent = originalParent;
                    onMovingPlatform = false;
                }
            }
        }

        // Ensure `grounded` stays true if player is on a moving platform
        if (onMovingPlatform)
        {
            grounded = true;
        }
    }

    private void HandleInput()
    {
        HandleJumpInput();
        UpdateMovementState();
    }

    private void HandleJumpInput()
    {
        if (inputManager.IsJumpPressed() && canJump && grounded)
        {
            Jump();
            canJump = false;
            Invoke(nameof(ResetJump), jumpCoolDown);
        }
    }

    private void UpdateMovementState()
    {
        bool canStand = CanStandUp();
        if (grounded)
        {
            if (inputManager.IsCrouchPressed() || !canStand)
            {
                state = MovementState.crouching;
                moveSpeed = crouchSpeed;
                SetPlayerScale(crouchYScale);
            }
            else if (inputManager.IsSprintPressed())
            {
                state = MovementState.sprinting;
                moveSpeed = sprintSpeed;
                AdjustPlayerScale();
            }
            else
            {
                state = MovementState.walking;
                moveSpeed = walkSpeed;
                AdjustPlayerScale();
            }
        }
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
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

        LimitSpeed();
    }

    private void ControlDrag()
    {
        rb.drag = grounded ? groundDrag : airDrag;
    }

    private void LimitSpeed()
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
        transform.localScale = Vector3.Lerp(transform.localScale,
            new Vector3(transform.localScale.x, startYScale, transform.localScale.z), Time.deltaTime * 10f);
    }

    private void SetPlayerScale(float newYScale)
    {
        transform.localScale = new Vector3(transform.localScale.x, newYScale, transform.localScale.z);
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
