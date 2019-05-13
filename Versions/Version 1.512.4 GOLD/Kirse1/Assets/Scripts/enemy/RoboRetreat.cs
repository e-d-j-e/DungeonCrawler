using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboRetreat : MonoBehaviour
{
    float startTime = 3;
    float moveDelay = 0;
    Transform player;
    Vector3 attPos;
    Rigidbody2D rigidBody;
 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        rigidBody = GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame
    void Update()
    {
        moveDelay -= Time.deltaTime;
        attPos = player.transform.position - transform.position;
        

        if (gameObject.name == "range2")
        {
            if (Vector3.Distance(transform.position, player.position) < 10)
            {
                transform.position += attPos.normalized * 7 * Time.deltaTime;
                Vector3 theScale = transform.localScale;
                if (moveDelay <= 0) DashAway();
                if (transform.position.x < player.transform.position.x)
                {
                    theScale.x = -1;
                    transform.localScale = theScale;
                }
                else
                {
                    theScale.x = 1;
                    transform.localScale = theScale;
                }
            }
        }
        else if (Vector3.Distance(transform.position, player.position) < 8)
        {
            transform.position += -attPos.normalized * 2 * Time.deltaTime;
            Vector3 theScale = transform.localScale;
            if (moveDelay <= 0) DashAway();
            if (transform.position.x < player.transform.position.x)
            {
                theScale.x = -1;
                transform.localScale = theScale;
            }
            else
            {
                theScale.x = 1;
                transform.localScale = theScale;
            }
        }

    }
    void DashAway()
    {
        rigidBody.velocity = -attPos.normalized * 15;
        moveDelay = startTime;
        Invoke("ResetVelocity", .3F);
    }
    void ResetVelocity()
    {
        rigidBody.velocity = Vector3.zero;
    }
}
