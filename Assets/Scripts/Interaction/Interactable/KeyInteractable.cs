using UnityEngine;

public class KeyInteractable : MonoBehaviour, IInteractable
{
    public string keyID; // Unique ID for the key
    private bool isCollected = false;
    public float obtainableScale; // This is the maximum size the player is able to pick up the key at

    public void Interact()
    {
        if (!isCollected)
        {
            if (GetComponent<ObjectSize>() != null)
            {
                if(transform.localScale.x <= obtainableScale)
                {
                    CollectKey();
                }
            }
            else
            {
                CollectKey();
            }
        }
    }

    public void CollectKey()
    {
        isCollected = true;
        InventorySystem.Instance.AddKey(keyID); // Add key to player's inventory
        gameObject.SetActive(false); // Hide the key after collection
    }

    public string GetDescription()
    {
        return isCollected ? "" : "Press E to pick up the key";
    }
}
