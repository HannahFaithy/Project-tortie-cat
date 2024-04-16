using UnityEngine;

public class MC_Controller : MonoBehaviour
{
    public float moveSpeed = 5f;                          // Movement speed of the character
    public float jumpForce = 5f;                          // Force applied when the character jumps
    public float runSpeedMultiplier = 2f;                 // Multiplier for movement speed when running
    public Transform groundCheck;                         // Transform used to check if the character is grounded
    public LayerMask groundLayer;                         // Layer mask for identifying ground
    public float interactRange = 2f;                      // Range for interacting with objects
    public InventoryObject playerInventory;               // The player's inventory
    public GameObject inventoryUI;                        // UI element for the inventory

    private Rigidbody rb;                                 // Reference to the character's Rigidbody component
    private Animator animator;                             // Reference to the character's Animator component
    private bool isGrounded;                               // Flag indicating if the character is grounded
    private bool isRunning;                                // Flag indicating if the character is running
    private bool isInventoryOpen;                          // Flag indicating if the inventory is open
    private PickUpObject currentPickupObject;              // The current object the character can pick up

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        inventoryUI.SetActive(false);                      // Initially hide the inventory UI
        CloseInventory();                                   // Close the inventory
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
        rb.MovePosition(transform.position + movement);     // Move the character's position

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360f);   // Rotate the character towards the movement direction
        }

        animator.SetBool("isWalking", movement != Vector3.zero);   // Set the "isWalking" parameter in the animator
    }

    private void HandleJump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);   // Check if the character is grounded

        if (Input.GetButtonDown("Jump") && isGrounded)        // Jump if the jump button is pressed and the character is grounded
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("isJumping", true);              // Set the "isJumping" parameter in the animator
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
            animator.SetBool("isRunning", true);               // Set the "isRunning" parameter in the animator
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
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange))   // Cast a ray forward to detect objects within the interact range
        {
            PickUpObject pickupObject = hit.collider.GetComponent<PickUpObject>();            // Check if the detected object can be picked up
            if (pickupObject != null)
            {
                pickupObject.PickUp();                              // Pick up the object
                animator.SetBool("isPicking", true);                 // Set the "isPicking" parameter in the animator
            }
        }
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
        inventoryUI.SetActive(true);                        // Show the inventory UI
        isInventoryOpen = true;
        Time.timeScale = 0f;                                 // Pause the game
    }

    private void CloseInventory()
    {
        inventoryUI.SetActive(false);                       // Hide the inventory UI
        isInventoryOpen = false;
        Time.timeScale = 1f;                                 // Resume the game's time scale
    }

    public void SetCurrentPickupObject(PickUpObject pickupObject)
    {
        currentPickupObject = pickupObject;
    }

    public void SetInventory(InventoryObject inventoryObject)
    {
        playerInventory = inventoryObject;
    }

    public void Interact()
    {
        if (currentPickupObject != null)
        {
            currentPickupObject.PickUp();                      // Pick up the current object
            currentPickupObject = null;
        }
    }

    public void AddItemToInventory(Item item, int amount)
    {
        playerInventory.AddItem(item, amount);                  // Add an item to the player's inventory
    }

    public void ClearCurrentPickupObject()
    {
        currentPickupObject = null;
    }
}