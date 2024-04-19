using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private MenuManager menuManager;

    void Start()
    {
        // Access the MenuManager component from the parent GameObject
        menuManager = transform.parent.GetComponent<MenuManager>();

        menuManager.background.SetActive(true);

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
        menuManager.ShowOptionMenu();
        menuManager.HideMainMenu();
    }

    public void OnQuitButtonClicked()
    {
        // Quit the game
        Debug.Log("Quit");
        Application.Quit();
    }
}
