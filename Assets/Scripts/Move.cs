using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
  private float velocity = 5f;
  private float velocityJump = 8f;
  private Vector3 angularVelocity = new Vector3(0f, 4f, 0f);
  private Vector3 direction = Vector3.zero;
  private Rigidbody rigidBody;
  private bool isGround = false;
  private int jumpTime = 0;
  private int jumpTimeMax = 10;
  private int jumpCount = 0;
  private int jumpCountMax = 2;
  private bool isJumping = false;
  private RaycastHit hit;
  private float rayDistance, rayScale;
  private Vector3 localGravity = new Vector3(0f, -20f, 0f);
  private bool isRotate = false;
  void Start () {
    rigidBody = GetComponent<Rigidbody>();
    rigidBody.useGravity = false;
    rayScale = transform.localScale.x * 0.5f;
    rayDistance = rayScale;
  }
  
  void Update () {
    // WASDで移動
    if (Input.GetKey(KeyCode.W)) {
      direction += transform.forward;
    }
    if (Input.GetKey(KeyCode.S)) {
      direction -= transform.forward;
    }
    if (Input.GetKey(KeyCode.A)) {
      direction -= transform.right;
    }
    if (Input.GetKey(KeyCode.D)) {
      direction += transform.right;
    }
    if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
      direction = Vector3.zero;
    }
    // スペースキーでジャンプ
    if (Input.GetKey(KeyCode.Space) && jumpCount < jumpCountMax) {
      if (jumpTime < jumpTimeMax) {
        jumpTime++;
        isJumping = true;
      } else {
        isJumping = false;
      }
    }
    if (Input.GetKeyUp(KeyCode.Space)) {
      jumpCount++;
      jumpTime = 0;
    }
    
    // if (Input.GetKey(KeyCode.LeftShift)) {
    // }
    // 回転 矢印キー左右
    // if (Input.GetKey(KeyCode.LeftArrow)) {
    //   // transform.Rotate(0, -angle, 0);
    //   isRotate = true;
    // }
    // if (Input.GetKey(KeyCode.RightArrow)) {
    //   // transform.Rotate(0, angle, 0);
    // }
    // if (Input.GetKeyUp(KeyCode.LeftArrow)) {
    //   // angularVelocity = new Vector3(0f, 5f, 0f);
    //   isRotate = false;
    // }

		isGround = Physics.BoxCast(transform.position, Vector3.one * rayScale, -transform.up, out hit, transform.rotation, rayDistance);
  }
  // void OnDrawGizmos () {
  //   Gizmos.DrawWireCube (transform.position - transform.up * hit.distance, Vector3.one * rayScale * 2);
  // }
  void FixedUpdate () {
    rigidBody.AddForce(localGravity, ForceMode.Acceleration);
    direction.Normalize();
    rigidBody.velocity = new Vector3(direction.x * velocity, rigidBody.velocity.y, direction.z * velocity);
		if (isGround) {
      jumpCount = 0;
    }
    if (isJumping) {
      rigidBody.velocity = new Vector3(rigidBody.velocity.x, velocityJump, rigidBody.velocity.z);
      isJumping = false;
    }
    
    
    // if (isRotate) {
    //   rigidBody.angularVelocity = angularVelocity;
    //   // rigidBody.AddTorque(Vector3.up * Mathf.PI / 180, ForceMode.VelocityChange);
    // }
    // Debug.Log(rigidBody.angularVelocity);

  }
}
