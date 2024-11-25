using UnityEngine;

public class QuantumPlatform : MonoBehaviour
{
    public float shrinkSpeed = 2f;    // Speed of shrinking and growing
    public float maxWidth = 5f;      // Maximum horizontal scale
    public float minWidth = 1f;      // Minimum horizontal scale

    private Transform player;
    private bool playerIsLooking = false;

    private float oscillationTimer = 0f; // Timer to control sine wave behavior

    private Renderer platformRenderer;

    private void Start()
    {
        platformRenderer = GetComponent<Renderer>();

        // Find the player object by tag
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

        // Check if the player is looking at the platform
        playerIsLooking = IsPlayerLooking();

        // Transform the platform unless the player is looking at it
        if (!playerIsLooking)
        {
            OscillateSize();
        }
    }

    private bool IsPlayerLooking()
    {
        // Check if the platform is in the player's view
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
                    return true; // Player is looking at the platform
                }
            }
        }

        return false;
    }

    private void OscillateSize()
    {
        // Increment the oscillation timer based on the shrink speed
        oscillationTimer += Time.deltaTime * shrinkSpeed;

        // Calculate the new size using a sine wave
        float newSize = Mathf.Lerp(minWidth, maxWidth, (Mathf.Sin(oscillationTimer) + 1) / 2);

        // Apply the size to the platform
        Vector3 scale = transform.localScale;
        scale.x = newSize;
        transform.localScale = scale;
    }
}
