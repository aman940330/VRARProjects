using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    //Prefab that the pool will use 
    public GameObject poolPrefab;

    //initial number of element
    public int intialNum = 10;


    List<GameObject> pooledObjects;

    // init pool 
    void Awake()
    {
        // init list 
        pooledObjects = new List<GameObject>();

        // create this initial number of objects 
        for(int i = 0; i < intialNum; i++)
        {
            // create a new object 
            CreateObj();
            

            
        }
    }

    // create a new object 
    GameObject CreateObj()
    {
        // create a new object 
        GameObject newObj = Instantiate(poolPrefab);

        // set this new object to inactive 

        newObj.SetActive(false);
        // add it to the list 

        pooledObjects.Add(newObj);

        return newObj;
    }

    // retrieve an object from the pool 
    public GameObject GetObj()
    {
        // search our list for an inactive object 
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            // if we find an inactive object 
            if (!pooledObjects[i].activeInHierarchy)
            {
                // enable it (set it to be active)
                pooledObjects[i].SetActive(true);
                // return that object 
                return pooledObjects[i];
            }


        }


        // increase our pool (create a new object)
        GameObject newObj = CreateObj();

        // enable that new object 
        newObj.SetActive(true);
        // return that object 
        return newObj;
    }

}
