using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/New Item")]
public class ItemObject : ScriptableObject
{
    [Serializable]
    public class ItemData
    {
        public int Id;
        public ItemBuff[] buffs;
        // Other properties related to the item's data
    }

    public Sprite uiDisplay;
    public GameObject characterDisplay;
    public bool stackable;
    public ItemType type;
    [TextArea(15, 20)] public string description;
    public int Id; // Updated field name to match the 'Item' class

    public List<string> boneNames = new List<string>();
    public ItemData data = new ItemData(); // Nested ItemData instance

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }

    private void OnValidate()
    {
        boneNames.Clear();
        if (characterDisplay == null)
            return;
        if (!characterDisplay.TryGetComponent<SkinnedMeshRenderer>(out var renderer))
            return;

        var bones = renderer.bones;

        foreach (var bone in bones)
        {
            boneNames.Add(bone.name);
        }
    }
}