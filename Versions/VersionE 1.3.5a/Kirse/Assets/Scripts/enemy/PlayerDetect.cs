using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour {
    private enemyAI enemy;
    private void Start()
    {
        enemy = GetComponentInChildren<enemyAI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            enemy.attack = true;
        }
    }
}
