using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InventoryManager : MonoBehaviour
{
    public Inventory Inventory;
    public List<Item> itemList;
    public List<int> itemAmounts;

    // Start is called before the first frame update
    void Start()
    {
        itemList = new List<Item>();

        // Instantiate items from the inventory and add them to the itemList
        foreach (var pair in Inventory.inventoryItems)
        {
            itemList.Add(pair.Key);
        }

        // Create a copy of the Inventory scriptable object
        Inventory = ScriptableObject.Instantiate(Inventory);
    }

    public void AddItem(ItemManagerment M)
    {
        Item item = M.item;

        // Check if the item already exists in the inventory
        bool itemExists = false;
        for (int i = 0; i < Inventory.inventoryItems.Count; i++)
        {
            if (Inventory.inventoryItems[i].Key == item)
            {
                // Increase amount of the existing item in the inventory list
                var kvp = Inventory.inventoryItems[i];
                Inventory.inventoryItems[i] = new KeyValuePair<Item, int>(kvp.Key, kvp.Value + item.amount);
                Debug.Log("Existing item amount increased: " + item.name + ", New amount: " + Inventory.inventoryItems[i].Value);
                itemExists = true;
                break;
            }
        }

        if (!itemExists)
        {
            // Add the item to the itemList
            itemList.Add(item);

            // Add the item to the inventory list with its initial amount
            Inventory.inventoryItems.Add(new KeyValuePair<Item, int>(item, item.amount));
            Debug.Log("New item added to inventory: " + item.name + ", Amount: " + item.amount);
        }
    }

    public void RemoveItem(Item item)
    {
        itemList.Remove(item);
    }

    public void UpdateInventoryUI()
    {
        // Update UI for each slot
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform slot = transform.GetChild(i);

            // Find the TextMeshProUGUI component within the second child of the slot
            TextMeshProUGUI textMeshPro = slot.GetChild(1).GetComponent<TextMeshProUGUI>();

            // Check if the index is within the range of items
            if (i < itemList.Count)
            {
                // Get the item and its corresponding amount
                Item item = itemList[i];
                int amount = (i < itemAmounts.Count) ? itemAmounts[i] : 0;

                // Update the UI text with the item amount
                if (textMeshPro != null)
                {
                    textMeshPro.text = amount.ToString();
                }
            }
            else
            {
                // Clear the UI text if no item exists in this slot
                if (textMeshPro != null)
                {
                    textMeshPro.text = "";
                }
            }
        }
    }
}
