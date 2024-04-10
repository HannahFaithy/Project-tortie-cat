using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryViewer : MonoBehaviour
{
    public GameObject slotPrefab; // prefab of the slot
    public Transform slotsParent; // parent which slots added to
    public int numSlots = 20; // number of slots

    private void Awake()
    {
        SpawnSlots();
    }
    void SpawnSlots()
    {
        for (int i = 0; i < numSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotsParent);
        }
    }

    private void OnEnable()
    {
        InventoryManager inv = MC_Controller.Controller.GetComponent<InventoryManager>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Image sprite = child.GetComponentInChildren<Image>();
            if (i < inv.itemList.Count)
                sprite.sprite = inv.itemList[i].icon;
        }
    }
}
