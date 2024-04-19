using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private MenuManager menuManager;

    void Start()
    {
        // Access the MenuManager component from the parent GameObject
        menuManager = transform.parent.GetComponent<MenuManager>();

        menuManager.background.SetActive(true);

        // check if MenuManager component is found
        if (menuManager == null)
        {
            Debug.Log("MenuManager not found in the scene");
        }
    }

    public void ShowOptionMenu()
    {
        menuManager.ShowOptionMenu();
        menuManager.HidePauseMenu();
    }

    public void ShowMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        menuManager.HidePauseMenu();
    }

    public void OnQuitButtonClicked()
    {
        // Quit the game
        Debug.Log("Quit");
        Application.Quit();
    }
}
