using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite icon;
    public int maxStackSize; // max stack size
    public int quantity; // current stack size
    public Mesh mesh;
    public List<Material> materials;

    public bool IsStackable()
    {
        return maxStackSize > 1;
    }
}
