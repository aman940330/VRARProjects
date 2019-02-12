using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour {

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            FighterController.instance.React();
            EnemyController.instance.EnemyReact();
            Debug.Log("Hit");
        }
    }

        

}
