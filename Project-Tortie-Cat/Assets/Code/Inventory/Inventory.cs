using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject
{
    public List<KeyValuePair<Item, int>> inventoryItems = new List<KeyValuePair<Item, int>>();
}