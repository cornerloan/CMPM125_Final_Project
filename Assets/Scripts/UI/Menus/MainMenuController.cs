using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene Settings")]
    public string gameSceneName = "GameScene";  
    public VisualTreeAsset optionsMenuAsset;    

    private VisualElement mainMenuContainer;
    private Button playButton;
    private Button settingsButton;
    private Button creditsButton;
    private Button quitButton;

    private void OnEnable()
    {
        // Get the root of the UI Document
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Find Main Menu Container
        mainMenuContainer = root.Q<VisualElement>("MainMenuContainer");

        // Assign button elements
        playButton = mainMenuContainer.Q<Button>("PlayBtn");
        settingsButton = mainMenuContainer.Q<Button>("OptionsBtn");
        creditsButton = mainMenuContainer.Q<Button>("CreditsBtn");
        quitButton = mainMenuContainer.Q<Button>("QuitBtn");

        // Assign callback methods to buttons
        playButton.clicked += StartGame;
        settingsButton.clicked += OpenSettings;
        creditsButton.clicked += ShowCredits;
        quitButton.clicked += QuitGame;
    }

    private void OnDisable()
    {
        // Unsubscribe from events to avoid memory leaks
        playButton.clicked -= StartGame;
        settingsButton.clicked -= OpenSettings;
        creditsButton.clicked -= ShowCredits;
        quitButton.clicked -= QuitGame;
    }

    private void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    private void OpenSettings()
    {
        if (optionsMenuAsset != null)
        {
            VisualElement optionsMenu = optionsMenuAsset.CloneTree();
            mainMenuContainer.Add(optionsMenu);
        }
    }

    private void ShowCredits()
    {
        SceneManager.LoadScene("CreditsMenu");
    }

    private void QuitGame()
    {
        Debug.Log("Quitting game...");
    }
}
