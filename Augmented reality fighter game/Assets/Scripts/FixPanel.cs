using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;

public class FixPanel : MonoBehaviour {

    public GameObject RefreshButton;
    public GameObject cameraButton;
    private string s = "";

	// Use this for initialization
	void Start () {
        s = SceneManager.GetActiveScene().name;
	}
	
	// Update is called once per frame
	void Update () {

        if (DefaultTrackableEventHandler.trueFalse==true)
        {
            cameraButton.SetActive(false);
        }
       
	}

    public void Refresh()
    {
        SceneManager.LoadScene("FighterGame");
    }
}
