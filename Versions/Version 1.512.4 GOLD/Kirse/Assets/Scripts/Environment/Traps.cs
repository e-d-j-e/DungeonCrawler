using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    BasicMovment player;
    SpriteRenderer sp;
    bool a; 
    private void Start()
    {
         sp = GameObject.FindGameObjectWithTag("trap").GetComponent<SpriteRenderer>();
         player = GameObject.FindGameObjectWithTag("Player").GetComponent<BasicMovment>();
         a =false;
    }    

    private IEnumerator OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player" && a==false )
        {
            a = true;
            sp.color = new Color32(30, 30, 255, 255);
            player.DecreaseHealth(3);
            yield return new WaitForSeconds(0.420f);
            a = false;
            //StartCoroutine(traps());
        }
    }

    private void OnTriggerExit2D(Collider2D o)
    {
        if (o.gameObject.tag == "Player")
        {
            sp.color = new Color32(255, 255, 255, 255);
        }
    }



}
