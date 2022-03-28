using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Variables
    private Rigidbody playerRigidbody;
    private Vector2 direction;
    private bool touching;
    // Config
    public float speed = 20;
    public float jumpSpeed = 20;

    // Awake (called before Start)
    private void Awake() {
        playerRigidbody = GetComponent<Rigidbody>();
    }
    // Fixed Update (Physics)
    public void FixedUpdate() {
        // Adds force to the player in the direction of the input (If no input, no force)
        playerRigidbody.AddForce(transform.forward * speed * Time.deltaTime * direction.y * 100);
        playerRigidbody.AddForce(transform.right * speed * Time.deltaTime * direction.x * 100);
    }
    // UpdateDirection (called by Input)
    public void UpdateDirection(InputAction.CallbackContext context) {
        // Sets direction to the input
        direction = context.ReadValue<Vector2>();
    }
    // Jump (called by Input)
    public void Jump() {
        // Only jump if touching the ground
        if(touching){
            // Adds force upwards to the player
            playerRigidbody.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
            touching = false;
        }
    }

    // Checks if the player is touching the ground
    private void OnCollisionEnter(Collision collision){if(collision.gameObject.tag == "Ground") {touching = true;}}
}
