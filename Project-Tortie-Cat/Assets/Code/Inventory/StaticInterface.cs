using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Declaration of the class "StaticInterface" that extends "UserInterface"
public class StaticInterface : UserInterface
{
    // Public array of GameObjects representing slots
    public GameObject[] slots;

    // Override method to create slots in the interface
    public override void CreateSlots()
    {
        // Commented out: Call the base implementation of CreateSlots() to ensure any necessary initialization is performed.

        // Create a new dictionary to store slots on the interface
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

        // Iterate over the inventory slots
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            // Get the GameObject representing the slot from the slots array
            var obj = slots[i];

            // Add event triggers to the slot GameObject
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            // Assign the slot display of the inventory slot to the slot GameObject
            inventory.GetSlots[i].slotDisplay = obj;

            // Add the slot GameObject and corresponding inventory slot to the slotsOnInterface dictionary
            slotsOnInterface.Add(obj, inventory.GetSlots[i]);
        }
    }
}
