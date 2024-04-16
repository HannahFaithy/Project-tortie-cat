using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Inventory Inventory;
    public List<Item> itemList;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Inventory.items.Count; i++)
        {
            itemList.Add(ScriptableObject.Instantiate(Inventory.items[i]));
        }

       Inventory = ScriptableObject.Instantiate(Inventory);
    }

    public void AddItem(ItemManagerment M)
    {
        Item item = M.item;
        /*if (item.IsStackable())
        {
            // Check if the item already exists in the inventory
            Item existingItem = itemList.Find(i => i.name == item.name && i.quantity < i.maxStackSize);
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
        }*/

        // If not stackable or no existing stack found, add as a new item
        itemList.Add(item);
    }

    public void RemoveItem(Item item)
    {
        itemList.Remove(item);
    }
}
