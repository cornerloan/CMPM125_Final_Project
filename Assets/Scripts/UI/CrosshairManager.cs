using UnityEngine;
using UnityEngine.UI;

public class CrosshairManager : MonoBehaviour
{
    public Image crosshairDefault;   // Default crosshair
    public Image crosshairInteract; // Crosshair for interactable objects
    public float interactionRange = 5f; // Max range for detecting interactable objects

    private Camera playerCamera;
    private bool isInteractableCrosshairActive = false; // Tracks current crosshair state

    private void Start()
    {
        playerCamera = Camera.main;

        // Ensure only the default crosshair is visible initially
        SetDefaultCrosshair();
    }

    private void Update()
    {
        UpdateCrosshair();
    }

    private GameObject lastOutlinedObject;

    private void UpdateCrosshair()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                // Handle outline effect
                if (hit.collider.gameObject != lastOutlinedObject)
                {
                    if (lastOutlinedObject != null)
                    {
                        var previousOutline = lastOutlinedObject.GetComponent<OutlineEffect>();
                        previousOutline?.DisableOutline();
                    }

                    var currentOutline = hit.collider.GetComponent<OutlineEffect>();
                    currentOutline?.EnableOutline();
                    lastOutlinedObject = hit.collider.gameObject;
                }

                SetInteractCrosshair();
                return;
            }
        }

        // Reset crosshair and remove outline
        if (lastOutlinedObject != null)
        {
            var outline = lastOutlinedObject.GetComponent<OutlineEffect>();
            outline?.DisableOutline();
            lastOutlinedObject = null;
        }

        SetDefaultCrosshair();
    }

    private void SetDefaultCrosshair()
    {
        crosshairDefault.enabled = true;
        crosshairInteract.enabled = false;
    }

    private void SetInteractCrosshair()
    {
        crosshairDefault.enabled = false;
        crosshairInteract.enabled = true;
    }
}
