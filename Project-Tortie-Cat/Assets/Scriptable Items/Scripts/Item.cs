[System.Serializable]
public class Item
{
    public string Name;
    public int Id = -1;
    public ItemBuff[] buffs;
    public ItemObject itemObject; // Add a reference to the ItemObject

    public Item()
    {
        Name = "";
        Id = -1;
    }

    public Item(ItemObject itemObject)
    {
        Name = itemObject.name;
        Id = itemObject.data.Id;
        buffs = new ItemBuff[itemObject.data.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(itemObject.data.buffs[i].Min, itemObject.data.buffs[i].Max)
            {
                stat = itemObject.data.buffs[i].stat
            };
        }
        this.itemObject = itemObject; // Assign the reference to the ItemObject
    }
}

