using UnityEngine;

public class PickupInteractable : MonoBehaviour, IInteractable
{
    private bool isPickedUp = false;
    private Transform playerHand; // Assign this in the inspector or programmatically

    public void Interact()
    {
        if (!isPickedUp)
        {
            isPickedUp = true;
            transform.SetParent(playerHand);
            transform.localPosition = Vector3.zero; // Move to player's hand position
            transform.localRotation = Quaternion.identity;
        }
        else
        {
            isPickedUp = false;
            transform.SetParent(null); // Drop the item
        }
    }

    public string GetDescription()
    {
        return isPickedUp ? "Press E to Drop" : "Press E to Pick Up";
    }
}
