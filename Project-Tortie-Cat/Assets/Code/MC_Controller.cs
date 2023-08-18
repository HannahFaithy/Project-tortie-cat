using System;
using UnityEngine;

public class MC_Controller : MonoBehaviour
{
    // Movement speed of the character
    public float moveSpeed = 5f;
    // Force applied when the character jumps
    public float jumpForce = 5f;
    // Multiplier for movement speed when running
    public float runSpeedMultiplier = 2f;
    // Transform used to check if the character is grounded
    public Transform groundCheck;
    // Layer mask for identifying ground
    public LayerMask groundLayer;
    // Range for interacting with objects
    public float interactRange = 2f;
    // The player's inventory
    public InventoryObject playerInventory;
    // UI element for the inventory
    public GameObject inventoryUI;

    // Reference to the character's Rigidbody component
    private Rigidbody rb;
    // Reference to the character's Animator component
    private Animator animator;
    // Flag indicating if the character is grounded
    private bool isGrounded;
    // Flag indicating if the character is running
    private bool isRunning;                                
    // Flag indicating if the inventory is open
    private bool isInventoryOpen;                          
    // The current object the character can pick up
    private GroundItem currentGroundItem;              

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        // Initially hide the inventory UI
        inventoryUI.SetActive(false);                      
        // Close the inventory
        CloseInventory();                                   
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        HandleRun();
        HandleInventory();
        HandlePickup();
        CheckAnimationState();
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float currentSpeed = isRunning ? moveSpeed * runSpeedMultiplier : moveSpeed;
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * currentSpeed * Time.deltaTime;
        // Move the character's position
        rb.MovePosition(transform.position + movement);     

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement);
            // Rotate the character towards the movement direction
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360f);   
        }

        // Set the "isWalking" parameter in the animator
        animator.SetBool("isWalking", movement != Vector3.zero);   
    }

    private void HandleJump()
    {
        // Check if the character is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);   

        // Jump if the jump button is pressed and the character is grounded
        if (Input.GetButtonDown("Jump") && isGrounded)        
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // Set the "isJumping" parameter in the animator
            animator.SetBool("isJumping", true);              
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }

    private void HandleRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isRunning = true;
            // Set the "isRunning" parameter in the animator
            animator.SetBool("isRunning", true);               
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
        }
    }

    private void HandleInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI.activeSelf)
                CloseInventory();
            else
                OpenInventory();
        }
    }

    private void HandlePickup()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }
    }

    private void TryPickup()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange))
        {
            GroundItem groundItem = hit.collider.GetComponent<GroundItem>();
            if (groundItem != null && groundItem.isPickupable)
            {
                PickUp(groundItem);
            }
        }
    }

    private void PickUp(GroundItem groundItem)
    {
        ItemObject itemObject = groundItem.itemObject;
        int amount = 1;

        playerInventory.AddItem(itemObject.CreateItem(), amount);
        Destroy(groundItem.gameObject);
    }

    private void CheckAnimationState()
    {
        if (animator.GetBool("isPicking") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Picking"))
        {
            animator.SetBool("isPicking", false);
            // Set other animation parameters or trigger appropriate transitions here
        }
    }

    private void OpenInventory()
    {
        // Show the inventory UI
        inventoryUI.SetActive(true);                        
        isInventoryOpen = true;
        // Pause the game
        Time.timeScale = 0f;                                 
    }

    private void CloseInventory()
    {
        // Hide the inventory UI
        inventoryUI.SetActive(false);                       
        isInventoryOpen = false;
        // Resume the game's time scale
        Time.timeScale = 1f;                                 
    }


    public void SetInventory(InventoryObject inventoryObject)
    {
        playerInventory = inventoryObject;
    }

    public void AddItemToInventory(Item item, int amount)
    {
        // Add an item to the player's inventory
        Console.WriteLine("Additem");
        playerInventory.AddItem(item, amount);                  
    }

    public void SetCurrentPickupObject(GroundItem groundItem)
    {
        currentGroundItem = groundItem;
    }

    public void ClearCurrentPickupObject()
    {
        currentGroundItem = null;
    }
}