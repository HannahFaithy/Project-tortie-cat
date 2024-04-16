using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private MenuManager menuManager;

    void Start()
    {
        // Find the MenuManager component in the scene
        menuManager = FindObjectOfType<MenuManager>();

        // Check if MenuManager component is found
        if (menuManager == null)
        {
            Debug.LogError("MenuManager not found in the scene!");
        }
    }

    public void OnPlayButtonClicked()
    {
        Debug.Log("play game");
        SceneManager.LoadScene("Main");
    }

    public void OnOptionsButtonClicked()
    {
        // Check if MenuManager reference is valid
        if (menuManager != null)
        {
            menuManager.ShowOptionMenu();
        }
        else
        {
            Debug.LogWarning("MenuManager reference is null!");
        }
    }

    public void OnQuitButtonClicked()
    {
        // Quit the game
        Debug.Log("Quit");
        Application.Quit();
    }
}
