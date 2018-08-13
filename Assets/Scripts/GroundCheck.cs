using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
	private RaycastHit hit;
	private float rayDistance;
	// Use this for initialization
	void Start () {
		rayDistance = transform.localScale.y * 0.51f;
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = new Ray(transform.position, -transform.up);
		Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.green, 5, false);
		if (Physics.Raycast(ray, out hit, rayDistance)) {
			Debug.Log("ground");
		} else {
			Debug.Log("not ground");
		}
	}
}
