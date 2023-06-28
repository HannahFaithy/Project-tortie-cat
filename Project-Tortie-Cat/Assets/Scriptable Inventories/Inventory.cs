using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Slots = new InventorySlot[24];  // Array of inventory slots
    private List<InventoryObject> inventoryObjects = new List<InventoryObject>();  // List of inventory objects

    public void Clear()
    {
        foreach (InventorySlot slot in Slots)
        {
            slot.item = new Item();  // Clear the item in each slot
            slot.amount = 0;        // Reset the amount to zero
        }
    }

    public bool ContainsItem(ItemObject itemObject)
    {
        return Array.Find(Slots, i => i.item.Id == itemObject.data.Id) != null;  // Check if any slot contains the item based on its ID
    }

    public bool ContainsItem(int id)
    {
        return Slots.FirstOrDefault(i => i.item.Id == id) != null;  // Check if any slot contains the item based on its ID
    }

    public void SetInventoryObject(InventoryObject inventoryObject)
    {
        inventoryObjects.Add(inventoryObject);  // Add the inventory object to the list
        // Additional code to handle the inventory object being added to the inventory
    }

    public void AddItem(ItemObject itemObject, int amount)
    {
        // Logic to add the item to the inventory
        // For example, find an empty slot or stack it with an existing item
    }
}