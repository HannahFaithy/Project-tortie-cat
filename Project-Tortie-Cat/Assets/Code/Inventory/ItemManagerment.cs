using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ItemManagerment : MonoBehaviour
{
    private MC_Controller mcController;
    public Item item;

    private void Start()
    {
        mcController = FindObjectOfType<MC_Controller>();
    }

    private void OnMouseDown()
    {
        if (mcController != null)
        {
            mcController.SetCurrentPickupObject(this);

            // Perform any additional actions related to the pickup
            // For example:
            // Show a pickup prompt UI
        }
    }

    public void PickUp()
    {
        // Get reference to the player's inventory
        InventoryManager inventory = MC_Controller.Controller.GetComponent<InventoryManager>();

        if (inventory != null)
        {
            // Add the item to the player's inventory
            inventory.AddItem(this.GetComponent<ItemManagerment>());
            // Disable the objct when it's picked up
            gameObject.SetActive(false);
        }               
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        BoxCollider collider = GetComponent<BoxCollider>();

        filter.mesh = item.mesh;
        renderer.materials = item.materials.ToArray();
        collider.size = item.mesh.bounds.size * 0.8f;
    }
#endif

}
