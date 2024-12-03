using UnityEngine;

public class MoveInteractable : MonoBehaviour, IInteractable
{
    private bool isHeld = false;
    private Rigidbody rb;
    private float holdDistance = 2f; // Distance in front of the player

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Interact()
    {
        isHeld = !isHeld; // Toggle the held state

        if (isHeld)
        {
            // Disable physics when held
            rb.useGravity = false;
            rb.isKinematic = true;
        }
        else
        {
            // Enable physics when released
            rb.useGravity = true;
            rb.isKinematic = false;
        }
    }

    public bool IsHeld()
    {
        return isHeld;
    }

    public void UpdateHeldPosition(Transform playerCamera)
    {
        if (isHeld)
        {
            // Move the object smoothly to a position in front of the camera
            Vector3 targetPosition = playerCamera.position + playerCamera.forward * holdDistance;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
            transform.rotation = Quaternion.Lerp(transform.rotation, playerCamera.rotation, Time.deltaTime * 10f);
        }
    }

    public string GetDescription()
    {
        return isHeld ? "Press E to Release" : "Press E to Move";
    }
}
