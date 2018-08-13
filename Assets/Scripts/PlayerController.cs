using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private float speed = 3.0f;
	private float gravity = 9.81f;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	void Update () {
		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}
}
