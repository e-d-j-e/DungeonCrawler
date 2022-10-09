using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour 
{
    public BasicMovment test;
    Transform player;
    float Speed = .4f, dist=.3f;

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

            if (Vector3.Distance(transform.position, player.position) > dist)
            {
                transform.Translate(new Vector3(Speed * Time.deltaTime, 0, 0));


            }
        //}

            //= Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotSpeed * Time.deltaTime);

       // transform.position += transform.forward * moveSpeed * Time.deltaTime;

    }
}
