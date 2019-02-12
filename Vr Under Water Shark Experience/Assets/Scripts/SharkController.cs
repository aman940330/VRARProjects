using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SharkController : FishController {

	bool isChasing = false;

	protected override void HandlePlayerNearby ()
	{
		// set chasing flag 
		isChasing = true;

		// get animator component 
		Animator anim = GetComponent<Animator>();

		// set the parameter 
		anim.SetBool("SawPlayer", true);

	}

	protected override void FixedUpdate(){

		if (isChasing) {
			// if we are chasing, look at the player 
			transform.LookAt (Camera.main.transform.position);

			// direction of velocity 
			Vector3 dir = Camera.main.transform.position - transform.position;

			// update the speed vector = normalised vector * speed 

			velocity = dir.normalized * speed; 
		}

		base.FixedUpdate ();


	}

	protected override void OnTriggerEnter (Collider other)
	{
		// call the parent method 
		base.OnTriggerEnter (other);

		if (other.CompareTag ("Player")) {

			// restart scene 
			SceneManager.LoadScene("Game");
		}
			
	}
}
