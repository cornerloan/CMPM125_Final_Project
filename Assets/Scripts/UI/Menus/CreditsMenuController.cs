using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class CreditsMenuController : MonoBehaviour
{
    [Header("Scene Settings")]
    public VisualTreeAsset optionsMenuAsset;

    private VisualElement creditsMenuContainer;
    private Button returnButton;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        creditsMenuContainer = root.Q<VisualElement>("CreditsMenuContainer");

        if (creditsMenuContainer == null)
        {
            return;
        }

        returnButton = creditsMenuContainer.Q<Button>("ReturnBtn");

        returnButton.clicked += ReturnMain;
    }

    private void OnDisable()
    {
        // Unsubscribe from events to avoid memory leaks
        if (returnButton != null)
        {
            returnButton.clicked -= ReturnMain;
        }
    }

    private void ReturnMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
