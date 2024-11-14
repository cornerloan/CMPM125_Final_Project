using System.Diagnostics;
using UnityEngine;

public class ExamineInteractable : MonoBehaviour, IInteractable
{
    public string examinationText = "This is an examinable object.";

    public void Interact()
    {
        // Display examination text on UI (e.g., an overlay)
        UnityEngine.Debug.Log(examinationText);
    }

    public string GetDescription()
    {
        return "Press E to Examine";
    }
}
