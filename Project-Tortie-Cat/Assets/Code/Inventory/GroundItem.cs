using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    // Public variable to hold the item object
    // Reference to the scriptable object representing the item
    public ItemObject itemObject;
    // lets scripts know that item is pick upable
    public bool isPickupable = true;

    public Item Item => itemObject.CreateItem();

    private MC_Controller mcController;

    // Property to retrieve the uiDisplay sprite
    public Sprite UIDisplaySprite => itemObject.uiDisplay;

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
        // Add the item to the player's inventory
        MC_Controller playerController = FindObjectOfType<MC_Controller>();
        if (playerController != null)
        {
            Item item = Item;
            playerController.AddItemToInventory(item, 1);
            // Optionally, you can perform other actions when the object is picked up
            gameObject.SetActive(false);
        }
    }

    // This method is automatically called by Unity when a value is changed in the Inspector
    // It is used here to update the sprite displayed in the Unity Editor for easier distinction of items on the ground
    private void OnValidate()
    {
#if UNITY_EDITOR
        // Get the SpriteRenderer component of the child object and assign the UIDisplaySprite to it
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
            spriteRenderer.sprite = UIDisplaySprite;
#endif
    }
}
