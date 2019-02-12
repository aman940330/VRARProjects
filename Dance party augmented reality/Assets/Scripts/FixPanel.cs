using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;

public class FixPanel : MonoBehaviour {


    public GameObject RefreshButton;
    private string s = "";
    private bool onOff = false;

	// Use this for initialization
	void Start () {
        s = SceneManager.GetActiveScene().name;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void refresh()
    {
        SceneManager.LoadScene(s);
    }

    public void ToggleFlash() {

        if (onOff==false)
        {
            CameraDevice.Instance.SetFlashTorchMode(true);
            onOff = true;
        }
        else
        {
            CameraDevice.Instance.SetFlashTorchMode(false);
            onOff = false;
        }
    }
}
