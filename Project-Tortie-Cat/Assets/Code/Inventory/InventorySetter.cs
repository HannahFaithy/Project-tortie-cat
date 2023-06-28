using UnityEngine;

// Declaration of the class "InventorySetter" which inherits from MonoBehaviour
public class InventorySetter : MonoBehaviour
{
    // Public variables to hold references to the inventory object, pick-up object, and MC_Controller object
    public InventoryObject inventoryObject;
    public MC_Controller mcController;

    // This method is called when the object is started in the scene
    private void Start()
    {
        // Check if the mcController reference is not null
        if (mcController != null)
        {
            // Set the inventoryObject in the mcController
            mcController.SetInventory(inventoryObject);
        }
    }
}