using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    private bool isInMainMenu;

    [SerializeField] private Button backButton;

    void Start()
    {
        // Determine if options menu was accessed from main menu or pause menu
        isInMainMenu = SceneManager.GetActiveScene().name == "MainMenu";

        // Assign the appropriate onClick event for the Back button
        if (isInMainMenu)
        {
            // If accessed from main menu, assign onClick event to return to main menu
            backButton.onClick.AddListener(ReturnToMainMenu);
        }
        else
        {
            // If accessed from pause menu, assign onClick event to return to pause menu
            backButton.onClick.AddListener(ReturnToPauseMenu);
        }
    }

    // Function to handle returning to the main menu
    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Function to handle returning to the pause menu
    void ReturnToPauseMenu()
    {
        SceneManager.LoadScene("PauseMenu");
    }
}
