using UnityEngine;

// Declaration of the class "InventorySetter" which inherits from MonoBehaviour
public class InventorySetter : MonoBehaviour
{
    // Public variables to hold references to the inventory object, pick-up object, and MC_Controller object
    public InventoryObject inventoryObject;
    public PickUpObject pickUpObject;
    public MC_Controller mcController;

    // This method is called when the object is started in the scene
    private void Start()
    {
        // Check if the pickUpObject reference is not null
        if (pickUpObject != null)
        {
            // Assign the inventoryObject to the pickUpObject's inventoryObject
            pickUpObject.inventoryObject = inventoryObject;
        }

        // Check if the mcController reference is not null
        if (mcController != null)
        {
            // Set the inventoryObject in the mcController
            mcController.SetInventory(inventoryObject);
        }
    }
}