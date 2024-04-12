using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionMenu;
    public GameObject pauseMenu;

    private GameObject previousMenu;

    void Start()
    {
        // Make sure only the main menu is active at the start
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);
        pauseMenu.SetActive(false);

        // Set the previous menu to null initially
        previousMenu = null;
    }

    private void Update()
    {
        //check for pause first
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePauseMenu();
        }
    }
    public void ShowMainMenu()
    {
        ShowMenu(mainMenu);
    }

    public void ShowOptionMenu()
    {
        // Set the previous menu to the current active menu
        previousMenu = GetCurrentActiveMenu();
        ShowMenu(optionMenu);
    }

    public void ShowPauseMenu()
    {
        // Set the previous menu to the current active menu
        previousMenu = GetCurrentActiveMenu();
        ShowMenu(pauseMenu);
    }

    public void TogglePauseMenu()
    {
        // Store the current active menu as the previous menu
        previousMenu = GetCurrentActiveMenu();

        // Toggle the pause menu
        if (pauseMenu.activeSelf)
        {
            // If the pause menu is active, hide it and show the previous menu
            ShowPreviousMenu();
        }
        else
        {
            // If the pause menu is not active, show it
            ShowPauseMenu();
        }
    }

    public void ShowPreviousMenu()
    {
        if (previousMenu != null)
        {
            ShowMenu(previousMenu);
            previousMenu = null; // Reset the previous menu after showing it
        }
        else
        {
            Debug.LogWarning("Previous menu is null.");
        }
    }

    private void ShowMenu(GameObject menu)
    {
        mainMenu.SetActive(menu == mainMenu);
        optionMenu.SetActive(menu == optionMenu);
        pauseMenu.SetActive(menu == pauseMenu);
    }

    private GameObject GetCurrentActiveMenu()
    {
        if (mainMenu.activeSelf) return mainMenu;
        if (optionMenu.activeSelf) return optionMenu;
        if (pauseMenu.activeSelf) return pauseMenu;
        return null;
    }
}
