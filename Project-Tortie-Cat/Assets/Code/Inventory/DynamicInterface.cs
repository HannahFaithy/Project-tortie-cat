using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

// Declaration of the class "DynamicInterface" which inherits from "UserInterface"
public class DynamicInterface : UserInterface
{
    // Reference to the prefab used for displaying inventory slots
    public GameObject inventoryPrefab;

    // Method to create the interface and display inventory slots based on the inventory object
    public override void CreateSlots()
    {
        //base.CreateSlots(); 
        // Call the base implementation of CreateSlots() to ensure any necessary initialization is performed.

        GridLayoutGroup gridLayout = GetComponent<GridLayoutGroup>();
        // Get the GridLayoutGroup component attached to the DynamicInterface object.

        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

        // Initialize the inventory slots outside the loop
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i] = new InventorySlot(i); // Initialize the inventory slot with the index
        }

        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            GameObject obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            // Create a new instance of the inventoryPrefab and attach it to the DynamicInterface's transform.

            obj.GetComponent<RectTransform>().localPosition = Vector3.zero;
            // Reset the local position of the instantiated object to zero.

            RectTransform rectTransform = obj.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(gridLayout.cellSize.x - gridLayout.spacing.x, gridLayout.cellSize.y - gridLayout.spacing.y);
            // Set the size of the object based on the cell size and spacing defined by the GridLayoutGroup.

            int columnCount = gridLayout.constraintCount;
            int row = i / columnCount;
            int column = i % columnCount;
            rectTransform.localPosition = new Vector3(gridLayout.cellSize.x * column, -gridLayout.cellSize.y * row, 0f);
            // Calculate the position of the slot based on the GridLayoutGroup's constraints and index.

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            // Add event triggers for different interactions to the instantiated object.

            inventory.GetSlots[i].slotDisplay = obj;
            Debug.Log("Slot Display: " + obj.name + " -> " + inventory.GetSlots[i].Index);
            // Assign the instantiated object as the slot display for the corresponding inventory slot.

            slotsOnInterface.Add(obj, inventory.GetSlots[i]);
        }
    }
}
