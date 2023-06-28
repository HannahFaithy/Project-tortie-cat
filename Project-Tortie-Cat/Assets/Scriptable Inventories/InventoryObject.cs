using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;  // The path to save the inventory data
    public ItemDatabaseObject database;  // The item database containing item information
    public InterfaceType type;
    [SerializeField] private InventorySlot[] slots = new InventorySlot[0];  // Array of inventory slots
    public InventorySlot[] GetSlots => slots;  // Get the array of inventory slots

    public bool AddItem(Item item, int amount)
    {
        if (EmptySlotCount <= 0)
            return false;

        InventorySlot slot = FindItemOnInventory(item);
        if (!database.ItemObjects[item.Id].stackable || slot == null)
        {
            GetEmptySlot().UpdateSlot(item, amount);
            return true;
        }

        slot.AddAmount(amount);
        return true;
    }

    public int EmptySlotCount => GetSlots.Count(slot => slot.item.Id <= -1);  // Get the count of empty slots in the inventory

    public InventorySlot FindItemOnInventory(Item item)
    {
        return GetSlots.FirstOrDefault(slot => slot.item.Id == item.Id);  // Find the slot containing the specified item
    }

    public bool IsItemInInventory(ItemObject item)
    {
        return GetSlots.Any(slot => slot.item.Id == item.data.Id);  // Check if the specified item is in the inventory
    }

    public InventorySlot GetEmptySlot()
    {
        return GetSlots.FirstOrDefault(slot => slot.item.Id <= -1);  // Get the first empty slot in the inventory
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
            }
            stream.Close();
        }
    }

    public void Clear()
    {
        foreach (InventorySlot slot in slots)
        {
            slot.RemoveItem();  // Remove items from all slots in the inventory
        }
    }
}