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

    /*
    public void UpdateInventoryUI()
    {

    }
    */
}
