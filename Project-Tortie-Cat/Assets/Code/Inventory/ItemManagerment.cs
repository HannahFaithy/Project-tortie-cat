using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagerment : MonoBehaviour
{
    private MC_Controller mcController;
    public Item item;

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
        // Get reference to the player's inventory
        Inventory inventory = FindObjectOfType<Inventory>();

        if (inventory != null)
        {
            // Add the item to the player's inventory
            inventory.AddItem(this.GetComponent<Item>());
        }

        // Disable the object when it's picked up
        gameObject.SetActive(false);
    }
}
