using UnityEngine;

public class ElevatorButtonInteractable : MonoBehaviour, IInteractable
{
    public ElevatorController elevatorController;

    public void Interact()
    {
        // Trigger the elevator movement (up or down based on current state)
        if (elevatorController != null)
        {
            elevatorController.TriggerElevator();
        }
    }

    public string GetDescription()
    {
        return "Press E to activate the elevator";
    }
}
