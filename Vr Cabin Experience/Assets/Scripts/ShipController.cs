using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenvaVR{

	[RequireComponent(typeof(Rigidbody))]
	
public class ShipController : MonoBehaviour {

		public ButtonController[] btns; 

		// speed of the ship 
		public float speed;

		// rotation speed 
		public float angle;

		Rigidbody rb; 

		void Awake(){
			// get rigidbody 

			rb = GetComponent<Rigidbody> ();
		}
		void OnEnable(){

			for (int i = 0; i < btns.Length; i++) {
				btns [i].OnPress += Move;
			}
				
		}

		void OnDisable(){
			for (int i = 0; i < btns.Length; i++) {
				btns [i].OnPress -= Move;
			}

		}

		void Move(Vector3 dir, float rotation){
			// move ship 
			rb.AddForce (speed * dir, ForceMode.VelocityChange);

			// rotate ship 
			Quaternion rot = Quaternion.Euler(0, angle * rotation,0);
			rb.MoveRotation (rb.rotation * rot);
		}


}
}