using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionMenu;
    public GameObject pauseMenu;
    public GameObject background;

    //stores the scene we in.
    private string previousScene;
    //store the last menu
    private GameObject previousMenu;

    void Start()
    {
        // All menus are set to not show.
        mainMenu.SetActive(false);
        optionMenu.SetActive(false);
        pauseMenu.SetActive(false);
        background.SetActive(false);

        // Set the previous menu to null initially
        previousScene = null;
        
        if (SceneManager.GetActiveScene().name == "Main")
        {
            mainMenu = null;
        }
        else if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            pauseMenu = null;
            mainMenu.SetActive(!mainMenu.activeSelf);
            background.SetActive(!background.activeSelf);
        }
    }

    private void Update()
    {
        //check if option menu or pause menu are already active
        if (pauseMenu != null && pauseMenu.activeSelf || optionMenu != null && optionMenu.activeSelf)
        {
            // Pause menu or option menu is already active, do nothing
            return;
        }

        //check for pause first
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseMenu();
        }
        else
        {

        }
    }
    public void ShowMainMenu()
    {
        if (mainMenu != null)
        {
            previousScene = SceneManager.GetActiveScene().name;
            previousMenu = GetCurrentActiveMenu();
            ShowMenu(mainMenu);
            ShowMenu(background);
        }
    }

    public void ShowOptionMenu()
    {
        // store the current active menu as the previous menu
        previousMenu = GetCurrentActiveMenu();

        //show the option menu
        if (optionMenu != null)
        {
            ShowMenu(optionMenu);
            ShowMenu(background);
        }
        else
        {
            Debug.LogWarning("OptionMenu reference is null");
        }
        
    }

    public void HideOptionMenu()
    {
        if (optionMenu != null)
        {
            optionMenu.SetActive(false);
            background.SetActive(false);
        }

    }

    public void ShowPauseMenu()
    {
        if (pauseMenu != null)
        {
            previousScene = SceneManager.GetActiveScene().name;
            previousMenu = GetCurrentActiveMenu();
            ShowMenu(pauseMenu);
            ShowMenu(background);
        }        
    }

    public void HidePauseMenu()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
            background.SetActive(false);
        }
    }

    public void HideMainMenu()
    {
        if (optionMenu != null)
        {
            mainMenu.SetActive(false);
            background.SetActive(false);
        }

    }

    public void ShowPreviousMenu()
    {
        //check last menu wasn't null
        if (previousMenu != null)
        {
            //show previous menu
            ShowMenu(previousMenu);
        }
        else
        {
            Debug.LogWarning("previous menu is null");
        }
    }

    private void ShowMenu(GameObject menu)
    {
        if (menu == mainMenu && mainMenu != null)
        {
            mainMenu.SetActive(true);
        }
        else if (menu == optionMenu && optionMenu != null)
        {
            optionMenu.SetActive(true);
        }
        else if (menu == pauseMenu && pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
    }

    private GameObject GetCurrentActiveMenu()
    {
        if (mainMenu != null && mainMenu.activeSelf) return mainMenu;
        if (pauseMenu != null && pauseMenu.activeSelf) return pauseMenu;
        return null;
    }
}
