using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour {
    //public int damage = 65;
    //private tokens toke = new tokens();


    private void OnTriggerEnter2D(Collider2D att)
    {
        enemyAI enemy = att.GetComponent<enemyAI>();
        //toke.incToken();

        if (enemy != null)
        {
            if (this.tag=="slash")
            {
                enemy.takeDamage(65);
                FindObjectOfType<AudioManager>().Play("Slash");

            }
            //else if (this.tag=="spinslash")
            //{
            //    enemy.takeDamage(120);
            //    FindObjectOfType<AudioManager>().Play("Slash");
            //}
        }
    }
}
