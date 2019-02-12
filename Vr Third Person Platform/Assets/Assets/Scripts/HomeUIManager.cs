using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

public class HomeUIManager : MonoBehaviour {

	// vr input component 
	VRInput vrinput;
    //start our level 1


	void Awake(){
		// find our vr input in the scene 
		vrinput = FindObjectOfType<VRInput> ();
	}


    public void StartGame()
    {
        SceneManager.LoadScene("Level1");

    }


	void update(){

		// Input on the Jump axis 
		float jAxis = Input.GetAxis("Jump");

		// If the key has been pressed 
		if(jAxis > 0)
		{
			vrinput.TriggerOnClick ();
		}

	}

	
	
	
}
