using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboRetreat : MonoBehaviour
{
    Transform player;
    Vector3 attPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        attPos = player.transform.position - transform.position;


        if (Vector3.Distance(transform.position, player.position) < 8)
        {
            transform.position += -attPos.normalized * 2 * Time.deltaTime;
            Vector3 theScale = transform.localScale;
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
}
