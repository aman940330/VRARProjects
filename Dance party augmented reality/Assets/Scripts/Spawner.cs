using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
public class Spawner : DefaultTrackableEventHandler {

    public Text myScore;
    public static Spawner instance;
    private GameObject dancer;
    private Animator anim;
    public GameObject Instruct;
    private bool fired = false;
    public bool isGamePlaying = false;
    void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
       
    }
    // Use this for initialization
    void Start () {
       
	}

    public void BeginGame()
    {
       
        GameController.instance.PlayMusic();
        Instruct.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(DefaultTrackableEventHandler.truefalse == true && fired == false)
        {
            SpawnProcess();
            fired = true;
        }
        else if(DefaultTrackableEventHandler.truefalse == false && fired == true)
        {
            fired = false;
            CancelInvoke();
        }
	}

    public void SpawnProcess()
    {
        InvokeRepeating("spawn", 2.5f, 2.5f);
        isGamePlaying = true;
    }

    void spawn()
    {
        GameObject x = (GameObject)Instantiate(Resources.Load("PointButton"));
        x.transform.parent = transform;

        RectTransform position = x.GetComponent<RectTransform>();
        position.localPosition = new Vector3(0, 10, 0);
        position.localScale = new Vector3(1,1,1);

    }

    public void addScore(int theScore)
    {
      
        myScore.text = (int.Parse(myScore.text) + theScore).ToString();
        RemoveKids();
    }

    private void RemoveKids()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void MakeMove(string danceMove)
    {
        dancer = GameObject.Find("UserDefinedTarget-1/aj@Gangnam Style");
        anim = dancer.GetComponent<Animator>();
        anim.SetTrigger(danceMove);
    }
}
