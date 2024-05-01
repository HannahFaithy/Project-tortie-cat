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
    public bool isStackable; //bool for if stackable
    public int amount; // current item aount that is to be picked up which is on the groun
    public Mesh mesh;
    public List<Material> materials;
}
