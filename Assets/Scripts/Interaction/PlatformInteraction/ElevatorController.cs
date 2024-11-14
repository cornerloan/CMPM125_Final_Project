using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 2f;

    private enum ElevatorState { Idle, MovingUp, MovingDown }
    private ElevatorState currentState = ElevatorState.Idle;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; // Ensure the Rigidbody is non-kinematic
        rb.useGravity = false;  // Disable gravity for elevator
    }

    private void Update()
    {
        switch (currentState)
        {
            case ElevatorState.MovingUp:
                MoveToPosition(endPoint.position);
                break;
            case ElevatorState.MovingDown:
                MoveToPosition(startPoint.position);
                break;
        }
    }

    public void TriggerElevator()
    {
        if (currentState == ElevatorState.Idle)
        {
            currentState = IsAtPosition(startPoint.position) ? ElevatorState.MovingUp : ElevatorState.MovingDown;
        }
        else
        {
            currentState = (currentState == ElevatorState.MovingUp) ? ElevatorState.MovingDown : ElevatorState.MovingUp;
        }
    }

    private void MoveToPosition(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.velocity = direction * speed;

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            rb.velocity = Vector3.zero;
            currentState = ElevatorState.Idle;
            transform.position = targetPosition; // Snap to target to avoid drifting
        }
    }

    private bool IsAtPosition(Vector3 position)
    {
        return Vector3.Distance(transform.position, position) < 0.1f;
    }
}
