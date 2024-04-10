using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> items = new List<Item>();

}