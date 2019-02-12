using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

    public GameObject HSPanel;
    private Animator anim;
    public Text HSText;
    public AudioClip clip;
    AudioSource audio;
    public static GameController instance;
    private float TimeLeft = 60;
    public Text Timer;
    public GameObject flashButton;
    public bool gameFinished = false;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        if(GamePreferences.getSave() == "" || GamePreferences.getSave() == null)
        {
            GamePreferences.save("0");
        }

    }


    // Use this for initialization
    void Start () {
         anim = HSPanel.GetComponent<Animator>();
        getHighScore();
        audio = GetComponent<AudioSource>();
        //TimeLeft = clip.length;
	}
	
	// Update is called once per frame
	void Update () {
        if(SceneManager.GetActiveScene().name == "Dancing Game")
        {
            if (Spawner.instance.isGamePlaying == true)
            {
                TimeLeft -= Time.deltaTime;

                Timer.text = "Time :" + TimeLeft.ToString("f2");
                //int minutes = Mathf.FloorToInt(TimeLeft / 60F);
                //int seconds = Mathf.FloorToInt(TimeLeft - minutes * 60);
                //Timer.text = string.Format("Timer : {0:0}:{1:00}", minutes, seconds);
                flashButton.SetActive(false);
            }
            else if (Spawner.instance.isGamePlaying == false)
            {
                flashButton.SetActive(true);
            }

            if (TimeLeft < 0)
            {
                gameOver();
            }
        }
        

        
	}

    public void PlayMusic()
    {
        audio.Play();
    }

    public void stopMusic()
    {
        audio.Stop();
    }
    public void playGame()
    {
        SceneManager.LoadScene("Dancing Game");

    }
    public void BackToIntro()
    {
        SceneManager.LoadScene("Introduction");

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void playHSPanel()
    {
        anim.SetTrigger("PlayHsAnim");
    }
    public void backHsPanel()
    {
        anim.SetTrigger("GoBack");
    }

    public void gameOver()
    {
        gameFinished = true;
        Timer.text = "Time : 0";
        stopMusic();
        Spawner.instance.CancelInvoke();
        playHSPanel();
        HSText.text = Spawner.instance.myScore.text.ToString();
        string a = GamePreferences.getSave();
        string b = HSText.text.ToString();
        if ((int.Parse(a))<(int.Parse(b)))
        {
            GamePreferences.save(HSText.text);
        }
        
    }

    private void getHighScore()
    {
        HSText.text = GamePreferences.getSave();
    }
}

public static class GamePreferences
{
    public static string HighScore = "0";

    public static void save(string score)
    {
        PlayerPrefs.SetString(GamePreferences.HighScore, score);
    }

    public static string getSave()
    {
        return PlayerPrefs.GetString(GamePreferences.HighScore);
    }
}
