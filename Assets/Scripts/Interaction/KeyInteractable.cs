using UnityEngine;

public class KeyInteractable : MonoBehaviour, IInteractable
{
    public string keyID; // Unique ID for the key
    private bool isCollected = false;

    public void Interact()
    {
        if (!isCollected)
        {
            isCollected = true;
            InventorySystem.Instance.AddKey(keyID); // Add key to player's inventory
            gameObject.SetActive(false); // Hide the key after collection
        }
    }

    public string GetDescription()
    {
        return isCollected ? "" : "Press E to pick up the key";
    }
}
