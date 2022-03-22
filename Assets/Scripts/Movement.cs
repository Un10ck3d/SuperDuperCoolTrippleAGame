using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    public Vector2 direction;
    private bool touching;
    public float speed = 20;
    public float jumpSpeed = 20;
    private void Awake() {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate() {
        playerRigidbody.AddForce(transform.forward * speed * Time.deltaTime * direction.y * 100);
        playerRigidbody.AddForce(transform.right * speed * Time.deltaTime * direction.x * 100);
    }
    public void Move(InputAction.CallbackContext context) {
        direction = context.ReadValue<Vector2>();
    }
    public void Jump() {
        if(touching){
            playerRigidbody.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision){touching = true;}
    private void OnCollisionExit(Collision collision){touching = false;}
}
