using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }
        public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0.0f; //pause the game
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1.0f; //resume game
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
