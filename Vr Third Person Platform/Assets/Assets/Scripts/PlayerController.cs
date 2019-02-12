﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // walking speed 
    public float walkSpeed;

    //jumping speed 
    public float jumpForce;

    //coin playing sound 
    public AudioSource coinSound;


    //Rigidbody component 
    Rigidbody rb;

    // Collider component 
    Collider col;

    // flag to keep track of key pressing 
    bool pressedJump = false;

    //size of the player 
    Vector3 size;

    //y that represents that you fell 
    float minY = -1.5f;

	// Use this for initialization
	void Start () {
        // Grab our component 
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        // get player size 
        size = col.bounds.size;

        // set the camera position 
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        WalkHandler();
        JumpHandler();
        FallHandler();

       
    }

    // check if the player fell
    void FallHandler()
    {
        if(transform.position.y <= minY)
        {
            // Game over!
            GameManager.instance.GameOver();
        }
    }

    // takes care of the walking logic
    void WalkHandler()
    {
        // Input on x (Horizontal)
        float hAxis = Input.GetAxis("Horizontal");





        // Input on z(Vertical)

        float vAxis = Input.GetAxis("Vertical");

        
        // check that we are moving 
        if(hAxis != 0 || vAxis !=0)
        {
            // Movement direction 
            //Vector3 direction = new Vector3(hAxis, 0, vAxis);

		    Vector3 movementV = (vAxis * walkSpeed * Time.deltaTime) * Camera.main.transform.forward;
			Vector3 movementH = (hAxis * walkSpeed * Time.deltaTime) * Camera.main.transform.right;

			Vector3 movement = movementV + movementH;
			movement.y = 0;
            // option 1: modify the transform
            //transform.forward = direction;

			// Calculate the new position 
			// transform.position is the old position 

			Vector3 newPos = transform.position + movement;

			// Move
			rb.MovePosition(newPos);


            // option 2 : using our rigid body 
			rb.rotation = Quaternion.LookRotation(movement);
        }
    }
    // Takes care of jumping handling 
    void JumpHandler()
    {
        // Input on the Jump axis 
        float jAxis = Input.GetAxis("Jump");

        // If the key has been pressed 
        if(jAxis > 0)
        {
            bool isGrounded = CheckGrounded();
            // make sure we are not already jumping 
            if (!pressedJump && isGrounded)
            {
                pressedJump = true;

                // jumping vector 
                Vector3 jumpVector = new Vector3(0, jAxis * jumpForce, 0);

                //apply force 
                rb.AddForce(jumpVector, ForceMode.VelocityChange);
            }
            

           
        }
        else
        {
            //set flag to false 
            pressedJump = false;
        }

        
    }
    bool CheckGrounded()
    {
        // location of all 4 corners 
        Vector3 corner1 = transform.position + new Vector3(size.x / 2, -size.y / 2 + 0.01f, size.z / 2);
        Vector3 corner2 = transform.position + new Vector3(-size.x / 2, -size.y / 2 + 0.01f, size.z / 2);
        Vector3 corner3 = transform.position + new Vector3(size.x / 2, -size.y / 2 + 0.01f, -size.z / 2);
        Vector3 corner4 = transform.position + new Vector3(-size.x / 2, size.y / 2 + 0.01f, -size.z / 2);

        // check if we are grounded
        bool grounded1 = Physics.Raycast(corner1, -Vector3.up, 0.02f);
        bool grounded2 = Physics.Raycast(corner2, -Vector3.up, 0.02f);
        bool grounded3 = Physics.Raycast(corner3, -Vector3.up, 0.02f);
        bool grounded4 = Physics.Raycast(corner4, -Vector3.up, 0.02f);

        return (grounded1 || grounded2 || grounded3 || grounded4);

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            // Increase our score 
            GameManager.instance.IncreaseScore(1);



            //Play sound 
            coinSound.Play();

            // Destroy coin 
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            
            //Game Over!

            GameManager.instance.GameOver();
        }
        else if (other.CompareTag("Goal"))
        {
            // Send player to the next level 
            GameManager.instance.IncreaseLevel();
        }
    }

    
}
