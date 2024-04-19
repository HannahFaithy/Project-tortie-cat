using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
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

    public void OnBackButtonClicked()
    {
        menuManager.ShowPreviousMenu();
        menuManager.HideOptionMenu();
        menuManager.background.SetActive(true);
    }
}