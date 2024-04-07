using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            // Check if the item already exists in the inventory
            Item existingItem = items.Find(i => i.name == item.name && i.quantity < i.maxStackSize);
            if (existingItem != null)
            {
                // Add to existing stack
                int remainingSpace = existingItem.maxStackSize - existingItem.quantity;
                if (remainingSpace >= item.quantity)
                {
                    existingItem.quantity += item.quantity;
                    return;
                }
                else
                {
                    existingItem.quantity = existingItem.maxStackSize;
                    item.quantity -= remainingSpace;
                }
            }
        }

        // If not stackable or no existing stack found, add as a new item
        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }
}