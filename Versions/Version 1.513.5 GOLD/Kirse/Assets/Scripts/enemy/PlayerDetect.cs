using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour {
    private enemyAI enemy;
    private bool s;
    private void Start()
    {
        enemy = GetComponentInChildren<enemyAI>();
        s = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {            
            enemy.attack = true;
            if (gameObject.tag == "Enemy" && s == true)
            {
                FindObjectOfType<AudioManager>().Play("Rocky");
                s = false;
            }
            else if(gameObject.tag == "EnemyM" && s == true)
            {
                //FindObjectOfType<AudioManager>().Play("Rocky");
                s = false;
            }
            else if (gameObject.tag == "EnemyT" && s == true)
            {
                //FindObjectOfType<AudioManager>().Play("Rocky");
                s = false;
            }
        }
    }
   
}
