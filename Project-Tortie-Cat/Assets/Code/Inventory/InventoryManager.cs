using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab; // prefab of the slot
    public Transform slotsParent; // parent which slots added to
    public int numSlots = 20; // number of slots
    public Inventory Inventory;
    public List<Item> itemList;
    // Start is called before the first frame update
    void Start()
    {
        SpawnSlots();
        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i] = ScriptableObject.Instantiate(itemList[i]);
        }
    }

    // Update is called once per frame
    void SpawnSlots()
    {
        for (int i = 0; i < numSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotsParent);
        }
    }
}
