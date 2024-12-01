using UnityEngine;

public class QuantumElevator : MonoBehaviour
{
    public float moveSpeed = 2f;      
    public float moveHeight = 5f;     

    private Vector3 startPosition;    
    private float oscillationTimer = 0f;
    private bool playerIsLooking = false;
    private bool playerIsColliding = false;

    private Transform player;

    private void Start()
    {
        // Save the start position of the elevator
        startPosition = transform.position;

        // Find the player object
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            Debug.LogError("Player not found! Ensure your player object has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (player == null) return;

        // Check if the player is looking at the elevator
        playerIsLooking = IsPlayerLooking();

        // Move the elevator unless the player is looking at it and on it
        if (!playerIsLooking && playerIsColliding == false)
        {
            MoveElevator();
        }
    }

    private bool IsPlayerLooking()
    {
        // Check if the elevator is in the player's view
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        bool inView = viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0;

        // Perform a Raycast to check for direct line of sight
        if (inView)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    return true; // Player is looking at the elevator
                }
            }
        }

        return false;
    }

    private void MoveElevator()
    {
        // Increment the oscillation timer
        oscillationTimer += Time.deltaTime * moveSpeed;

        // Calculate the new vertical position using a sine wave, constrained between original and max height
        float offset = Mathf.Sin(oscillationTimer) * (moveHeight / 2f); // Scale oscillation to half of the height
        float newY = startPosition.y + (moveHeight / 2f) + offset;      // Offset ensures movement stays within bounds

        // Apply the new position
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
