using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    //score text label
    public Text scorelabel;

	// Use this for initialization
	void Start () {

        // start with the correct score 
        ResetHud();
	}

    // Show up to date stats of the player 

    public void ResetHud()
    {
        scorelabel.text = " Score:" + GameManager.instance.score;
        
    }
	
}
