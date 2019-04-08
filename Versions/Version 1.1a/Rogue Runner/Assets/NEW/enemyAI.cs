using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ethan 
//March 28, 2019
//Version 0.15.00
//Desc: Basic Enemy AI, follows player to real close. 
//New on new version:
//0.15.00- added new collisions 
//
//
public class enemyAI : MonoBehaviour 
{
   // public BasicMovment test;
    private Transform player;
    private float Speed = 1.4f, dist = .18f, maxDist = 10;
    public GameObject shotPrefab;

    public int health = 100;
    

    private Vector3 attPos;
    private float topVarY = 5;
    private float botVarY = -5;
    private float leftVarX = -5;
    private float rightVarX = 5;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update() 
    {       
            transform.LookAt(player.position);
            //transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            attPos = player.transform.position - transform.position;
        //if (.21 > Vector3.Distance(transform.position, player.position) && Vector3.Distance(transform.position, player.position) > -0.21)
        //{
        //    Debug.Log("Enemy hits you! Ouch!");
        //    //TAKE DAMAGE-TAKE KNOCKBACK-UPDATE HEALTH AND SCREEN AND SUCH-STUN ENEMY FOR A SECOND

        //}

        if (/*maxDist> Vector3.Distance(transform.position, player.position) &&*/ Vector3.Distance(transform.position, player.position) > dist)
        {
            //transform.Translate(new Vector3(Speed * Time.deltaTime, 0, 0));
           
        }
        
       //if (gameObject.tag=="RoboRange")
       // {

       //     GameObject attack = Instantiate(shotPrefab, transform.position, Quaternion.identity);
       //     attack.GetComponent<Rigidbody2D>().velocity = attPos * 1.5f;
       //     attack.transform.Rotate(0, 0, Mathf.Atan2(attPos.y, attPos.x) * Mathf.Rad2Deg);
       //     Destroy(attack, 0.32f);
       // }


       
       

    }

    private void OnCollisioEnter2D(Collider2D other)
    {         
        if (other.gameObject.tag == "Bottom Wall")
        {
            botVarY = transform.position.y;

        }
        else if (other.gameObject.tag == "Top Wall")
        {
            topVarY = transform.position.y;
        }
        else if (other.gameObject.tag == "Left Wall")
        {
            leftVarX = transform.position.x;
        }

        else if (other.gameObject.tag == "Right Wall")
        {
            rightVarX = transform.position.x;
        }
        
    }
   
    public void takeDamage(int damage)
    {
        health -= damage;
        if(health<=0)
        {
            Destroy(gameObject);
        }
    }





}
