using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];

    [System.NonSerialized]
    public UserInterface parent;
    [System.NonSerialized]
    public GameObject slotDisplay;

    [System.NonSerialized]
    public System.Action<InventorySlot> onAfterUpdated;
    [System.NonSerialized]
    public System.Action<InventorySlot> onBeforeUpdated;

    public Item item;
    public int amount;
    public int Index { get; private set; } // Add an Index property

    public InventorySlot(int index)
    {
        Index = index;
        Debug.Log("InventorySlot with index " + index + " initialized.");
    }

    public InventorySlot() : this(-1) // Call the other constructor with a default index of -1
    {
        Debug.Log("Empty InventorySlot initialized.");
    }

    public InventorySlot(Item item, int amount) : this(-1) // Call the other constructor with a default index of -1
    {
        UpdateSlot(item, amount);
        Debug.Log("InventorySlot with item " + item.Name + " and amount " + amount + " initialized.");
    }

    public ItemObject GetItemObject()
    {
        return item.Id >= 0 ? parent.inventory.database.ItemObjects[item.Id] : null;
    }

    public void RemoveItem()
    {
        if (item.Id >= 0)
        {
            Debug.Log("Removing item " + item.Name + " from slot.");
            item = new Item(); // Set the item to a default empty item
            amount = 0; // Reset the amount to zero
            Debug.Log("Item removed from slot. Slot is now empty.");
        }
        else
        {
            Debug.LogWarning("Attempting to remove item from an already empty slot.");
        }
    }

    public void AddAmount(int value) => UpdateSlot(item, amount += value);

    public void UpdateSlot(Item itemValue, int amountValue)
    {
        onBeforeUpdated?.Invoke(this);
        item = itemValue;
        amount = amountValue;
        onAfterUpdated?.Invoke(this);
    }

    public bool CanPlaceInSlot(ItemObject itemObject)
    {
        if (AllowedItems.Length <= 0 || itemObject == null || itemObject.data.Id < 0)
            return true;
        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (itemObject.type == AllowedItems[i])
                return true;
        }
        return false;
    }
}
