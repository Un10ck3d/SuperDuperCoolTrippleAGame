using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private bool touching;
    public float jumpSpeed = 10f;
    private void Awake() {
        playerRigidbody = GetComponent<Rigidbody>();
    }
    public void Jump() {
        if(touching){
            playerRigidbody.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision){touching = true;}
    private void OnCollisionExit(Collision collision){touching = false;}
}
