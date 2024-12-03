using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    public string requiredKeyID; // Key ID required to unlock the door
    public Transform openPosition; // Position or rotation when the door is open
    private bool isOpen = false;

    public void Interact()
    {
        if (isOpen) return;

        if (InventorySystem.Instance.HasKey(requiredKeyID))
        {
            isOpen = true;
            OpenDoor();
        }
        else
        {
            Debug.Log("You need a specific key to open this door.");
        }
    }

    private void OpenDoor()
    {
        // Smoothly open the door
        transform.position = openPosition.position;
        transform.rotation = openPosition.rotation;
    }

    public string GetDescription()
    {
        return isOpen ? "" : "Press E to open the door";
    }
}
