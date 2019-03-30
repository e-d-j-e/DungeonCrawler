using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ethan 
//March 28, 2019
//Version 0.10.00
//Desc: Basic Enemy AI, follows player to real close. 
//New on new version:
//
//
//
public class enemyAI : MonoBehaviour 
{
   // public BasicMovment test;
    Transform player;
    float Speed = .4f, dist=.18f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update() 
    {

       // if(test.isMove==true) {
            transform.LookAt(player.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        if (.21 > Vector3.Distance(transform.position, player.position) && Vector3.Distance(transform.position, player.position) > -0.21)
        {
            Debug.Log("Enemy hits you! Ouch!");
            //TAKE DAMAGE-TAKE KNOCKBACK-UPDATE HEALTH AND SCREEN AND SUCH-STUN ENEMY FOR A SECOND

        }

            else if (Vector3.Distance(transform.position, player.position) > dist)
            {
                transform.Translate(new Vector3(Speed * Time.deltaTime, 0, 0));
            }
        //}

            //= Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotSpeed * Time.deltaTime);

       // transform.position += transform.forward * moveSpeed * Time.deltaTime;

    }
}
