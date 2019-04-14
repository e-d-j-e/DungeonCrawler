using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HocusPokus : MonoBehaviour {
    public int damage = 20;


    private void OnTriggerEnter2D(Collider2D att)
    {
        enemyAI enemy = att.GetComponent<enemyAI>();
        if (enemy != null)
        {
            enemy.takeDamage(damage);            
        }
        
    }
}
