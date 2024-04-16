using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    private MenuManager menuManager;

    void Start()
    {
        // Access the MenuManager component from the parent GameObject
        menuManager = transform.parent.GetComponent<MenuManager>();
    }

    public void OnBackButtonClicked()
    {
        menuManager.ShowPreviousMenu();
    }
}