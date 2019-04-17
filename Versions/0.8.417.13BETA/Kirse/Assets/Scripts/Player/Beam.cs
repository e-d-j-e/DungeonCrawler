using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {
    public int damage = 45;
    

    private void OnTriggerEnter2D(Collider2D att)
    {
        enemyAI enemy = att.GetComponent<enemyAI>();
        //tokens toke = new tokens();
        
        if (enemy!=null)
        {
            enemy.takeDamage(damage);
            FindObjectOfType<AudioManager>().Play("Beam");
            Destroy(gameObject);
            //toke.incToken();
        }
        else if(att.gameObject.tag=="Top Wall"|| att.gameObject.tag == "Bottom Wall" || att.gameObject.tag == "Left Wall" || att.gameObject.tag == "Right Wall")
        {
            Destroy(gameObject);
        }
    }
}
