using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public float interactionRange = 3f;
    public LayerMask interactableLayer;
    private Camera playerCamera;
    public InputManager inputManager; 

    private IInteractable currentInteractable; 

    private void Start()
    {
        playerCamera = Camera.main;

        if (inputManager == null)
        {
            UnityEngine.Debug.LogError("InputManager not found in the scene!");
        }
    }

    private void Update()
    {
        // Check for interaction input through InputManager
        if (inputManager.IsInteractPressed())
        {
            if (currentInteractable != null)
            {
                // If already holding an object, release it
                currentInteractable.Interact();
                currentInteractable = null; // Reset the current interactable
            }
            else
            {
                // Otherwise, try to interact with a new object
                InteractWithObject();
            }
        }

        // Keep updating the position of the currently held object
        if (currentInteractable is MoveInteractable moveable && moveable.IsHeld())
        {
            moveable.UpdateHeldPosition(playerCamera.transform);
        }
    }

    private void InteractWithObject()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();

                // Store the interactable object if it’s a movable object
                if (interactable is MoveInteractable moveable)
                {
                    currentInteractable = moveable;
                }
            }
        }
    }
}
