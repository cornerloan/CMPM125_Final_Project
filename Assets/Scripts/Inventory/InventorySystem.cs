using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // Static instance that can be accessed from anywhere
    public static InventorySystem Instance { get; private set; }

    private HashSet<string> keys = new HashSet<string>(); // Track collected keys by ID
    private HashSet<string> items = new HashSet<string>(); // Track collected items by ID

    private void Awake()
    {
        // Check if the singleton instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy this instance if another one exists
            return;
        }

        Instance = this; // Assign this instance as the singleton instance

        // Optional: Make the singleton persist across scene loads
        DontDestroyOnLoad(gameObject);
    }

    public void AddKey(string keyID)
    {
        keys.Add(keyID);
        Debug.Log("Key collected: " + keyID);
    }

    public bool HasKey(string keyID)
    {
        return keys.Contains(keyID);
    }

    public void AddItem(string itemID)
    {
        items.Add(itemID);
        Debug.Log("Item collected: " + itemID);
    }

    public bool HasItem(string itemID)
    {
        return items.Contains(itemID);
    }

    public void RemoveItem(string itemID)
    {
        items.Remove(itemID);
    }
}
