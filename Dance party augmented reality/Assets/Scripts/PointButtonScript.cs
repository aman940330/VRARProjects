using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointButtonScript : MonoBehaviour {

    private int point = 500;
    private int[] points = { 500, 750, 1000, 1250, 2500, 5000 };
    private string[] moves = {"m1","m2","m3" };
    private string move;
    private Text theText;
    private float speed = 7f;
    private Image colour; 

	// Use this for initialization
	void Start () {
        point = points[Random.Range(0,5)];
        speed = Random.Range(7f, 18f);
        move = moves[Random.Range(0, 3)];
        theText = GetComponentInChildren<Text>();
        theText.text = point.ToString();

        colour = GetComponent<Image>();
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);

        colour.color = newColor;

        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(onTaskClicked);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-speed, 0f, 0f);
	}

    void onTaskClicked()
    {
        Spawner.instance.addScore(point);
        Spawner.instance.MakeMove(move);
    }
}
