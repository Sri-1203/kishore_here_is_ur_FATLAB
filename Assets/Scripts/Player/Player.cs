using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class Player : Entity {

    public float moveSpeed = 10f;
    public float verticalMouseSensitivity = 100f;
    public float horizontalMouseSensitivity = 100f;
    public float jumpHeight = 20f;
    public float distToGround = 2f;

    Vector3 moveDirection;
    float verticalLookRotation;

    private Camera cam;
    private Rigidbody rb;

    // Use this for initialization
    new void Start () {
        base.Start();
        rb = GetComponent<Rigidbody>();

        if (rb == null) {
            Debug.LogError("Rigidbody component is missing on the Player object.");
            return;
        }

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        cam = gameObject.GetComponentInChildren<Camera>();
    }
    
    // Update is called once per frame
    void Update () {
        // Get input for movement
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow

        // Debug logs to check input values
        Debug.Log($"Horizontal Input: {moveX}, Vertical Input: {moveZ}");

        // Create a movement direction based on input
        Vector3 inputVelocity = new Vector3(moveX, 0, moveZ).normalized;

        // Convert input direction from local to world space
        moveDirection = transform.TransformDirection(inputVelocity) * moveSpeed;

        // Debug logs to check calculated moveDirection
        Debug.Log("Calculated moveDirection: " + moveDirection);

        // Camera look rotation
        verticalLookRotation += Input.GetAxis("Mouse Y") * verticalMouseSensitivity * Time.deltaTime;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        cam.transform.localEulerAngles = Vector3.left * verticalLookRotation;

        // Horizontal rotation for player
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * horizontalMouseSensitivity * Time.deltaTime);
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();

        // Apply velocity to move the player
        if (rb != null)
        {
            rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);
        }
        
        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space) && onGround())
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            Debug.Log("Key Pressed: Space (Jump) | Position: " + transform.position);
        }
    }

    private bool onGround()
    {
        // Raycast to check if the player is close enough to the ground
        return Physics.Raycast(transform.position, -transform.up, distToGround + 0.1f);
    }

    public Inventory GetInventory()
    {
        return gameObject.GetComponent<Inventory>();
    }
private void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with an object tagged as "Alien"
        if (other.CompareTag("Alien"))
        {
            Debug.Log("Player touched the alien! Restarting game...");
            RestartGame();
        }
    }

    private void RestartGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
