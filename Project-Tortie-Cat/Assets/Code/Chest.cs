using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool generated = false;                 // Flag indicating if the chest's contents have been generated
    public bool open;                              // Flag indicating if the chest is open
    public ItemObject[] itemsInChest;               // Array of predefined items that can be found in the chest
    public List<Item> generatedItems = new List<Item>();  // List of items that have been generated and added to the chest's inventory
    public InventoryObject chestData;               // The chest's inventory object
    public DynamicInterface chestUi;                // The UI for the chest

    private void OnTriggerEnter(Collider other)
    {
        var newArray = new InventorySlot[itemsInChest.Length];
        // chestData.GetSlots = newArray;
        StartCoroutine(WaitOneFrame());
    }

    IEnumerator WaitOneFrame()
    {
        yield return new WaitForSeconds(0.2f);

        open = true;
        UpdateChestData();
        chestUi.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        open = false;
        chestUi.slotsOnInterface.Clear();
        chestData.Clear();
        foreach (Transform child in chestUi.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        chestUi.gameObject.SetActive(false);
    }

    private void UpdateChestData()
    {
        if (generated)
        {
            // Add generated items to the chest's inventory
            for (int i = 0; i < generatedItems.Count; i++)
            {
                chestData.AddItem(generatedItems[i], 1);
            }
            return;
        }

        // Add items from the predefined list to the chest's inventory
        for (int i = 0; i < itemsInChest.Length; i++)
        {
            Item item = new Item(itemsInChest[i]);
            chestData.AddItem(item, 1);
            generatedItems.Add(item);
        }

        generated = true;
    }

    private void Update()
    {
        if (!chestUi.isActiveAndEnabled || !open)
            return;

        // Remove generated items that are no longer in the chest's inventory
        for (int i = generatedItems.Count - 1; i >= 0; i--)
        {
            bool found = false;
            for (int j = 0; j < chestData.GetSlots.Length; j++)
            {
                if (generatedItems[i] == chestData.GetSlots[j].item)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                generatedItems.RemoveAt(i);
            }
        }
    }

    private void OnApplicationQuit()
    {
        chestData.Clear();
    }
}