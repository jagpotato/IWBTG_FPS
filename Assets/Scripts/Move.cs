using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
  private float velocity = 6f;
  private float angle = 1f;
  private float velocityX = 0f;
  private float velocityZ = 0f;
	private Vector3 direction = Vector3.zero;
  private Rigidbody rigidBody;
  private bool isGround = false;
  private int jumpTime = 0;
  private int jumpCount = 0;
  private bool isJumping = false;
  private RaycastHit hit;
  private float rayDistance;
  private float rayScale;
  void Start () {
    rigidBody = GetComponent<Rigidbody>();
    rayScale = transform.localScale.x * 0.5f;
    rayDistance = rayScale;
  }
  
  void Update () {
    if (Input.GetKey(KeyCode.W)) {
      velocityZ = velocity;
    }
    if (Input.GetKey(KeyCode.S)) {
      velocityZ = -velocity;
    }
    if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) {
      velocityZ = 0f;
    }
    if (Input.GetKey(KeyCode.A)) {
      velocityX = -velocity;
    }
    if (Input.GetKey(KeyCode.D)) {
      velocityX = velocity;
    }
    if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
      velocityX = 0f;
    }
		if (Input.GetKey(KeyCode.Space)) {
			if (jumpTime < 10) {
        jumpTime++;
        isJumping = true;
      } else {
        isJumping = false;
      }
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
      jumpCount++;
      jumpTime = 0;
      isJumping = false;
    }
    // if (Input.GetKey(KeyCode.LeftShift)) {
    // }
    // // 回転 矢印キー左右
    if (Input.GetKey(KeyCode.LeftArrow)) {
      transform.Rotate(0, -angle, 0);
    }
    if (Input.GetKey(KeyCode.RightArrow)) {
      transform.Rotate(0, angle, 0);
    }

    if (Physics.BoxCast(transform.position, Vector3.one * rayScale, -transform.up, out hit, transform.rotation, rayDistance)) {
      isGround = true;
      // Debug.Log("ground");
    } else {
      isGround = false;
      // Debug.Log("not ground");
    }
  }
  // void OnDrawGizmos () {
  //   Gizmos.DrawWireCube (transform.position - transform.up * hit.distance, Vector3.one * rayScale * 2);
  // }
  void FixedUpdate () {
		rigidBody.AddForce(new Vector3(0f, -20f, 0f), ForceMode.Acceleration);
		rigidBody.velocity = new Vector3(velocityX, rigidBody.velocity.y, velocityZ);
		if (isGround) {
			jumpCount = 0;
		}
		if (isJumping && jumpCount < 2) {
			rigidBody.velocity = new Vector3(rigidBody.velocity.x, 10f, rigidBody.velocity.z);
      // Debug.Log(jumpCount);
    }
  }
}
