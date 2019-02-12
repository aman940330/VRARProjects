﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenvaVR;

namespace ZenvaVR
{
    [RequireComponent(typeof(ObjectPool))]
    public class DistanceSpawner : MonoBehaviour
    {
        // reference game object
        public Transform reference;

        // minimim distance to spawn an object
        public float genDistance;

        // direction of the spawner
        public Vector3 direction;

        // min gap
        public float minGap;

        // max gap
        public float maxGap;

        // min scale
        public float minScale = 1;

        // max scale
        public float maxScale = 1;

        // spawning / disapp time
        public float timeStep = 1;

        // object pool component
        ObjectPool pool;

        // newly generated object
        GameObject newObj;

        // Use this for initialization
        void Awake()
        {
            //get the object pool component
            pool = GetComponent<ObjectPool>();

            //init pool
            pool.InitPool();

            //initial population of object
            while (Vector3.Distance(reference.position, transform.position) <= genDistance)
            {
                HandleSpawning();
            }

            //execute spawning and hiding only at certain frequency
            InvokeRepeating("HandleSpawning", 0, timeStep);
            InvokeRepeating("HandleHiding", 0, timeStep + 0.5f);
        }

        // Update is called once per frame
        void Update()
        {
            HandleSpawning();
            HandleHiding();
        }

        void HandleSpawning()
        {
            // Check distance
            if (Vector3.Distance(reference.position, transform.position) <= genDistance)
            {
                // Spawn object
                Spawn();

                // Reposition distance spawner
                Reposition();
            }
        }

        //handle deactivation of objects if they are too far
        void HandleHiding()
        {
            // get the active objects of the pool
            List<GameObject> actives = pool.GetAllActive();

            // go through all of them
            for (int i = 0; i < actives.Count; i++)
            {
                // check distance
                if (Vector3.Distance(reference.position, actives[i].transform.position) > genDistance)
                {
                    // deactivate them
                    actives[i].SetActive(false);
                }
            }
        }

        // reposition the spawner to it's next location
        void Reposition()
        {
            // move: 1) size of the new object 2) gap

            // gap
            float gap = UnityEngine.Random.Range(minGap, maxGap);

            // size of the model
            float size = 0;

            //get renderer of the object
            if (newObj.GetComponent<Renderer>() != null)
            {
                Vector3 dirFilter = Vector3.Scale(newObj.GetComponent<Renderer>().bounds.size, direction);
                size = Mathf.Max(dirFilter.x, dirFilter.y, dirFilter.z);
            }
            //get the renderer of the child
            else if (newObj.GetComponentInChildren<Renderer>())
            {
                Vector3 dirFilter = Vector3.Scale(newObj.GetComponentInChildren<Renderer>().bounds.size, direction);
                size = Mathf.Max(dirFilter.x, dirFilter.y, dirFilter.z);
            }

            // total distance we have to move it
            float total = gap + size;

            // repositioning
            transform.Translate(direction * total, Space.World);

        }

        // spawn a new object
        void Spawn()
        {
            //get an object from the pool
            newObj = pool.GetObj();

            //set position
            newObj.transform.position = transform.position;

            //generate a random scale number
            float scale = UnityEngine.Random.Range(minScale, maxScale);

            //scale object
            newObj.transform.localScale = Vector3.one * scale;
        }
    }
}