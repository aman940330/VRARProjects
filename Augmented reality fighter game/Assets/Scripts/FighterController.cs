using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterController : MonoBehaviour {

    public Transform enemyTarget;
    static Animator anim;
    public static bool mvBack = false;
    public static bool mvFWD = false;
    public static FighterController instance;
    public static bool isAttacking = false;
    private Vector3 direction;
    public int health = 100;
    public Slider playerHealthBar;
    public BoxCollider[] c;
    public AudioClip[] audioClip;
    AudioSource audio;
    private Vector3 playerPosition;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }

    

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        SetAllBoxCollider(false);
        audio = GetComponent<AudioSource>();
        playerPosition = transform.position;
	}

    public void playAudio(int clip)
    {
        audio.clip = audioClip[clip];
        audio.Play();
    }

    private void SetAllBoxCollider(bool state)
    {
        c[0].enabled = state;
        c[1].enabled = state;
    }

    // Update is called once per frame
    void Update () {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("fight_idle"))
        {
            direction = enemyTarget.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.5f);
        }
      
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("fight_idle"))
        {
            isAttacking = false;
            SetAllBoxCollider(false);
        }

        if(GameController.allowMovement == true)
        {
            if (isAttacking == false)
            {
                if (mvBack == true)
                {
                    anim.SetTrigger("WalkBack");
                    anim.ResetTrigger("idle");
                    SetAllBoxCollider(false);
                }

                else
                {
                    anim.SetTrigger("idle");
                    anim.ResetTrigger("WalkBack");
                }

                if (mvFWD == true)
                {
                    anim.SetTrigger("WalkForward");
                    anim.ResetTrigger("idle");
                    SetAllBoxCollider(false);
                }
                else if (mvBack == false)
                {
                    anim.SetTrigger("idle");
                    anim.ResetTrigger("WalkForward");
                }

            }
            else if (isAttacking == true)
            {
                SetAllBoxCollider(true);
            }

        }



    }

    public void punch()
    {
        isAttacking = true;
        anim.ResetTrigger("idle");
        anim.SetTrigger("Punch");
        playAudio(0);

    }

    public void kick()
    {
        isAttacking = true;
        anim.ResetTrigger("idle");
        anim.SetTrigger("Kick");
        playAudio(1);
    }

    public void React()
    {
        isAttacking = true;
        health = health - 10;
        if(health < 10)
        {
            KnockOut();
            playAudio(3);
        }
        else
        {
            anim.ResetTrigger("idle");
            anim.SetTrigger("react");
            playAudio(2);
        }
        playerHealthBar.value = health;

       
    }

    public void KnockOut()
    {
        GameController.allowMovement = false;
        health = 100;
        anim.SetTrigger("KnockOut");
        GameController.instance.scoreEnemy();
        GameController.instance.OnScreenPoints();
        GameController.instance.rounds();
        

        if (GameController.enemyScore == 2)
        {
            GameController.instance.doReset();
            StartCoroutine(resetCharacters());
        }
        else
        {
            StartCoroutine(resetCharacters());
        }
    }
    IEnumerator resetCharacters()
    {
        yield return new WaitForSeconds(4);
        playerHealthBar.value = 100;
        //reset position 
        GameObject[] theClone = GameObject.FindGameObjectsWithTag("Player");
        Transform t = theClone[5].GetComponent<Transform>();
        anim.SetTrigger("idle");
        anim.ResetTrigger("KnockOut");
        t.position = playerPosition;
        t.position = new Vector3(t.position.x, 0.1f, t.position.z);
        GameController.allowMovement = true;
    }
}
