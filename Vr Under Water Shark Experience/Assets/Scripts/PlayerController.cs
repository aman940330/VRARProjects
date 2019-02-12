using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {


	// movement speed 
	public float speed;

	// rigidBody component 
	Rigidbody rb;

	void Awake(){

		// grab our rigidbody 
		rb = GetComponent<Rigidbody>();
	}
		
	
	// Update is called once per frame
	void FixedUpdate () {
		HandleInput ();
	}

	void HandleInput(){
		if (Input.GetButtonDown ("Fire1")) {
			// set the velocity to the direction of camera times speed 
			rb.velocity = Camera.main.transform.forward * speed;

			// add an impulse to the rigidbody 
			// The following made me motion sick 
			//rb.AddForce(Camera.main.transform.forward * speed, ForceMode.Impulse);
		}
	}
}
