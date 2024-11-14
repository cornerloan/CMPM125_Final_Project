using UnityEngine;

public class PressurePlateInteractable : MonoBehaviour, IInteractable
{
    public ElevatorController elevatorController;
    public string playerTag = "Player"; // Tag used to detect the player on the pedal

    private bool isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if player steps on the pressure plate
        if (other.CompareTag(playerTag) && !isActivated)
        {
            isActivated = true;
            elevatorController.TriggerElevator(); // Trigger the elevator
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset activation when player leaves the plate
        if (other.CompareTag(playerTag))
        {
            isActivated = false;
        }
    }

    public void Interact() { }

    public string GetDescription() { return ""; } // No interaction text since it's automatic
}
