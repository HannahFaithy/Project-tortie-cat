using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button optionMenuButton;
    public Button mainMenuButton;

    private MenuManager menuManager;

    void Start()
    {
        // Find the MenuManager script in the scene
        menuManager = FindObjectOfType<MenuManager>();

        // Add onClick listeners to the buttons
        optionMenuButton.onClick.AddListener(ShowOptionMenu);
        mainMenuButton.onClick.AddListener(ShowMainMenu);
    }

    public void ShowOptionMenu()
    {
        menuManager.ShowOptionMenu();
    }

    public void ShowMainMenu()
    {
        menuManager.ShowMainMenu();
    }
    public void ShowPauseMenu()
    {
        menuManager.ShowPauseMenu();
    }
    public void OnQuitButtonClicked()
    {
        // Quit the game
        Debug.Log("Quit");
        Application.Quit();
    }
}
