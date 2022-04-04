using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Variables
    public Camera cam;
    private Rigidbody playerRigidbody;
    private Vector2 direction;
    public bool touching = false;
    public bool sprinting = false;
    // Config
    public float speed = 20;
    public float jumpSpeed = 40;
    public float maxSpeed = 20;

    // Awake (called before Start)
    private void Awake() {
        playerRigidbody = GetComponent<Rigidbody>();
        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Fixed Update (Physics)
    public void FixedUpdate() {
        // Adds force to the player in the direction of the input (If no input, no force)
        if (!(playerRigidbody.velocity.magnitude > maxSpeed)) {
            playerRigidbody.AddForce(transform.forward * speed * Time.deltaTime * direction.y * 100);
            playerRigidbody.AddForce(transform.right * speed * Time.deltaTime * direction.x * 100);
        }
    }
    // UpdateDirection (called by Input)
    public void UpdateDirection(InputAction.CallbackContext context) {
        // Sets direction to the input
        direction = context.ReadValue<Vector2>();
    }

    public void Update() {
        if (sprinting) {
            maxSpeed = 25;
            cam.fieldOfView = 70.0f;
        } else {
            maxSpeed = 20;
            cam.fieldOfView = 60.0f;
        }
    }

    // Jump (called by Input)
    public void Jump() {
        // Only jump if touching the ground
        if(touching){
            // Adds force upwards to the player
            playerRigidbody.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
            touching = false;
            maxSpeed = 18;
        }
    }
    public void Sprint(InputAction.CallbackContext context) {
        if (context.performed | context.started) {
            sprinting = true;
        }
        else {
            sprinting = false;
        }
    }

    // Checks if the player is touching the ground
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Ground") {touching = true; maxSpeed = 20;}
    }
}
