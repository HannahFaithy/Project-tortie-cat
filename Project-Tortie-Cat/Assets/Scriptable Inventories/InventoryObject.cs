using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;  // The path to save the inventory data
    public ItemDatabaseObject database;  // The item database containing item information
    public InterfaceType type;

    [SerializeField]
    private InventorySlot[] slots = new InventorySlot[10];  // Array of inventory slots
    private List<int> emptySlots = new List<int>();  // List of empty slot indices

    public InventorySlot[] GetSlots => slots;  // Get the array of inventory slots

    public InventoryObject() // Constructor to initialize the inventory with a specific number of slots
    {
        InitializeSlots();
        Debug.Log("Inventory slots initialized.");
    }

    private void InitializeSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new InventorySlot();
        }
    }

    public bool AddItem(Item item, int amount)
    {
        Debug.Log("EmptySlotCount (Before): " + EmptySlotCount);
        Debug.Log("EmptySlotCount: " + EmptySlotCount);
        Debug.Log("Empty Slots: " + string.Join(", ", emptySlots)); // Add this line

        // Check if the index is valid before accessing the array
        if (item.Id >= 0 && item.Id < GetSlots.Length)
        {
            InventorySlot slot = GetSlots[item.Id];
            if (!database.ItemObjects[item.Id].stackable || slot == null)
            {
                Debug.Log("Adding item to empty slot");
                GetEmptySlot().UpdateSlot(item, amount);
                return true;
            }

            slot.AddAmount(amount);
            return true;
        }
        else
        {
            Debug.LogError("Invalid item index: " + item.Id);
            return false;
        }
    }

    public int EmptySlotCount => emptySlots.Count;  // Get the count of empty slots in the inventory

    public InventorySlot FindItemOnInventory(Item item)
    {
        return GetSlots.FirstOrDefault(slot => slot.item.Id == item.Id);  // Find the slot containing the specified item
    }

    public bool IsItemInInventory(ItemObject item)
    {
        return GetSlots.Any(slot => slot.item.Id == item.Id);  // Check if the specified item is in the inventory
    }

    public InventorySlot GetEmptySlot()
    {
        if (emptySlots.Count > 0)
        {
            int emptySlotIndex = emptySlots[0];
            emptySlots.RemoveAt(0);
            return slots[emptySlotIndex];
        }
        return null;
    }

    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        if (item1 == item2)
            return;

        if (item2.CanPlaceInSlot(item1.GetItemObject()) && item1.CanPlaceInSlot(item2.GetItemObject()))
        {
            InventorySlot temp = new InventorySlot(item2.item, item2.amount);
            item2.UpdateSlot(item1.item, item1.amount);
            item1.UpdateSlot(temp.item, temp.amount);
        }
    }

    public void Save()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(Path.Combine(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, slots);  // Serialize the inventory slots and save them to a file
        stream.Close();
    }

    public void Load()
    {
        string filePath = Path.Combine(Application.persistentDataPath, savePath);
        if (File.Exists(filePath))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            InventorySlot[] newSlots = (InventorySlot[])formatter.Deserialize(stream);  // Deserialize the inventory slots from the file
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].UpdateSlot(newSlots[i].item, newSlots[i].amount);  // Update the slots with the loaded data
                if (newSlots[i].item.Id <= -1)
                {
                    emptySlots.Add(i);  // Add empty slot indices to the list
                }
            }
            stream.Close();
        }
    }

    public void Clear()
    {
        foreach (InventorySlot slot in slots)
        {
            slot.RemoveItem();  // Remove items from all slots in the inventory
            emptySlots.Add(slot.Index);  // Add emptied slot indices to the list
        }
    }
}
