using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{

    private MC_Controller mcController;

    private void Start()
    {
        mcController = FindObjectOfType<MC_Controller>();
    }

    private void OnMouseDown()
    {
        if (mcController != null)
        {
            mcController.SetCurrentPickupObject(this);

            // Perform any additional actions related to the pickup
            // For example:
            // Show a pickup prompt UI
        }
    }

    public void PickUp()
    {
        // Add the inventory object to the player's inventory
        MC_Controller playerController = FindObjectOfType<MC_Controller>();

        // Optionally, you can perform other actions when the object is picked up
        gameObject.SetActive(false);
    }

    // Remove the GetItemObject method from the script

}