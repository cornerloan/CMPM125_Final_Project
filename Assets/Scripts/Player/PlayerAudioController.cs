using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip walkSFX;        // Sound effect for walking
    public AudioClip sprintSFX;      // Sound effect for sprinting
    public AudioClip jumpSFX;        // Sound effect for jumping
    public AudioClip landSFX;        // Sound effect for landing
    public AudioClip interactSFX;    // Sound effect for interactions

    [Header("Audio Settings")]
    public float stepDistance = 1.5f; // Distance between footsteps
    public AudioSource audioSource;  // Audio source for playing sounds

    [Header("References")]
    public PlayerController playerController; // Reference to the player controller for state checking
    public InputManager inputManager;        // Reference to input manager for detecting interactions

    private Vector3 lastFootstepPosition;    // Position at the last footstep
    private bool wasGrounded;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (playerController == null)
        {
            Debug.LogError("PlayerController reference is missing! Assign it in the inspector.");
        }

        // Initialize the last footstep position
        lastFootstepPosition = playerController.transform.position;
        wasGrounded = playerController.grounded;
    }

    private void Update()
    {
        if (playerController == null) return;

        // Handle footstep sounds
        HandleFootsteps();

        // Handle jump and landing sounds
        HandleJumpAndLanding();

        // Handle interaction sound
        HandleInteraction();
    }

    private void HandleFootsteps()
    {
        // Only play footsteps when grounded and moving
        if (!playerController.grounded || !IsPlayerMoving())
        {
            // Reset the last footstep position to prevent sound spamming when standing still
            lastFootstepPosition = playerController.transform.position;
            return;
        }

        // Check the distance traveled since the last footstep
        float distanceTraveled = Vector3.Distance(playerController.transform.position, lastFootstepPosition);

        if (distanceTraveled >= stepDistance)
        {
            PlayFootstepSound();
            lastFootstepPosition = playerController.transform.position; // Update the last footstep position
        }
    }

    private bool IsPlayerMoving()
    {
        // Check if the player is moving by combining movement direction magnitude and position checks
        return playerController.moveDirection.magnitude > 0.1f &&
               Vector3.Distance(playerController.transform.position, lastFootstepPosition) > 0.01f;
    }

    private void PlayFootstepSound()
    {
        switch (playerController.state)
        {
            case PlayerController.MovementState.walking:
                PlaySound(walkSFX);
                break;
            case PlayerController.MovementState.sprinting:
                PlaySound(sprintSFX);
                break;
        }
    }



    private void HandleJumpAndLanding()
    {
        // Play landing sound when transitioning from air to grounded
        if (!wasGrounded && playerController.grounded)
        {
            if (!audioSource.isPlaying)
            {
                PlaySound(landSFX);
                Debug.Log("land");
            }
        }

        // Play jump sound immediately when SPACE is pressed
        if (inputManager.IsJumpPressed() && playerController.grounded)
        {
            if (!audioSource.isPlaying) { PlaySound(jumpSFX); Debug.Log("jump"); }
        }

        wasGrounded = playerController.grounded;
    }

    private void HandleInteraction()
    {
        // Play interaction sound when the interaction key (e.g., E) is pressed
        if (inputManager.IsInteractPressed())
        {
            if (!audioSource.isPlaying) PlaySound(interactSFX);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null || audioSource == null) return;

        audioSource.PlayOneShot(clip);
    }
}
