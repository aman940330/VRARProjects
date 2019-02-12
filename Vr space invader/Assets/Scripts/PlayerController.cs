
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenvaVR;


public class PlayerController : MonoBehaviour
{

    // Bullet velocity
    public float bulletSpeed = 10;

    // Gun
    public GameObject gun;

    // Game Manager
    GameManager gm;

    // Bullet Prefab
    //public GameObject bulletPrefab;


    // Object pool
    ObjectPool bulletPool;


    void Awake()
    {
        gm = GameObject.FindObjectOfType<GameManager>();

        // grab the pool component
        bulletPool = GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get user input
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // spawn a new bullet
            GameObject newBullet = bulletPool.GetObj();

            // pass the game manager
            newBullet.GetComponent<BulletController>().gm = gm;

            // position will be that of the gun
            newBullet.transform.position = gun.transform.position;

            // get rigid body
            Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();

            // give the bullet velocity
            bulletRb.velocity = gun.transform.forward * bulletSpeed;

        }
    }
}