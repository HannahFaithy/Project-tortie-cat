using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Declaration of the class "GroundItem" which inherits from MonoBehaviour
public class GroundItem : MonoBehaviour
{
    // Public variable to hold the item object
    public ItemObject item;

    // Property to retrieve the uiDisplay sprite
    public Sprite UIDisplaySprite => item.uiDisplay;

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
