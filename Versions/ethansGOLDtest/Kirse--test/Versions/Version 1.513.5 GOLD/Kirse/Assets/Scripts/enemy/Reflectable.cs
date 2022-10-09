using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflectable : MonoBehaviour
{
    GameObject player;
    bool reflected;

    private void Start()
    {
        player = GameObject.Find("Player");
        reflected = false;  
    }

    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyAI enemy = collision.GetComponent<enemyAI>();
        if (collision.gameObject.name == "HitBox")
        {
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Stop("chargebeam");
            player.GetComponent<BasicMovment>().DecreaseHealth(15);
        }
        else if(collision.gameObject.tag == "EnemyT" && reflected==true)
        {
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Stop("chargebeam");
            enemy.takeDamage(40);
        }

        if (collision.gameObject.tag == "slash")
        {
            Vector3 attPos = player.transform.position - transform.position;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            //should go back to turret enemy          
            gameObject.GetComponent<Rigidbody2D>().velocity = -attPos.normalized * 8;
            reflected = true;
        }

    }
    public void ChangeOrientation()
    {
        player = GameObject.Find("Player");
        float AngleRad = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, -AngleDeg);
    }
}
