using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour {

    public Transform playerTransform;
    private Vector3 direction;
    static Animator anim2;
    public int enemyHealth = 100;
    public static EnemyController instance;
    public Slider EnemyHealthBar;
    public BoxCollider[] c;
    public AudioClip[] audioClip;
    AudioSource audio1;
    private Vector3 enemyPosition;


    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Use this for initialization
    void Start () {
        anim2 = GetComponent<Animator>();
        SetAllBoxCollider(false);
        audio1 = GetComponent<AudioSource>();
        enemyPosition = transform.position;
	}

    public void playAudio(int clip)
    {
        audio1.clip = audioClip[clip];
        audio1.Play();
    }
    private void SetAllBoxCollider(bool state)
    {
       c[0].enabled = state;
       c[1].enabled = state;
    }
    // Update is called once per frame
    void Update () {
        if (anim2.GetCurrentAnimatorStateInfo(0).IsName("fight_idleCopy"))
        {
            direction = playerTransform.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.5f);
            SetAllBoxCollider(false);
        }

        Debug.Log(direction.magnitude);

        if (direction.magnitude > 13f && GameController.allowMovement == true)
        {
            anim2.SetTrigger("WalkForward");
            audio1.Stop();
            SetAllBoxCollider(false);
        }
        else
        {
            anim2.ResetTrigger("WalkForward");
        }

        if(direction.magnitude < 13f && direction.magnitude > 8 && GameController.allowMovement == true)
        {
           
            SetAllBoxCollider(true);
            if (!audio1.isPlaying &&!anim2.GetCurrentAnimatorStateInfo(0).IsName("roundhouse_kick 2"))
            {
                playAudio(1);
                anim2.SetTrigger("Kick");
            }
        }
        else
        {
            anim2.ResetTrigger("Kick");
        }

        if (direction.magnitude < 6f && GameController.allowMovement == true)
        {
            
            SetAllBoxCollider(true);
            if (!audio1.isPlaying && !anim2.GetCurrentAnimatorStateInfo(0).IsName("cross_punch"))
            {
                playAudio(0);
                anim2.SetTrigger("punch");
            }
        }

        else
        {
            anim2.ResetTrigger("punch");
        }

        if (direction.magnitude > 0f && direction.magnitude < 2 && GameController.allowMovement == true)
        {
            anim2.SetTrigger("WalkBack");
            SetAllBoxCollider(false);
            audio1.Stop();
        }
        else
        {
            anim2.ResetTrigger("WalkBack");
        }


    }

    public void EnemyReact()
    {
        enemyHealth = enemyHealth - 10;
        EnemyHealthBar.value = enemyHealth;
        if(enemyHealth < 10)
        {
            enemyKnockOut();
        }
        else
        {
            anim2.ResetTrigger("idle");
            anim2.SetTrigger("react");
        }
        
    }

    public void enemyKnockOut()
    {
        GameController.allowMovement = false;
        enemyHealth = 100;
       
        anim2.SetTrigger("KnockOut");
        GameController.instance.scorePlayer();
        GameController.instance.OnScreenPoints();
        GameController.instance.rounds();

        if(GameController.playerScore == 2)
        {
            GameController.instance.doReset();
        }
        else
        {
            StartCoroutine(resetCharacters());
        }
    }

    IEnumerator resetCharacters()
    {
        yield return new WaitForSeconds(4);
        EnemyHealthBar.value = 100;
        //reset position 
        GameObject[] theClone = GameObject.FindGameObjectsWithTag("Enemy");
        Transform t = theClone[0].GetComponent<Transform>();
        t.position = enemyPosition;
        t.position = new Vector3(t.position.x, 0.09f, t.position.z);
        GameController.allowMovement = true;
    }
}
