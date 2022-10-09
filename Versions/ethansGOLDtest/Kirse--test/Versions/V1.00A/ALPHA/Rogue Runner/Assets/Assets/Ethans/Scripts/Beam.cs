﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {
    public int damage = 45;
    

    private void OnTriggerEnter2D(Collider2D att)
    {
        enemyAI enemy = att.GetComponent<enemyAI>();
        if (enemy!=null)
        {
            enemy.takeDamage(damage);
            Destroy(gameObject);
        }
        else if(att.gameObject.tag=="Top Wall"|| att.gameObject.tag == "Bottom Wall" || att.gameObject.tag == "Left Wall" || att.gameObject.tag == "Right Wall")
        {
            Destroy(gameObject);
        }
    }
}