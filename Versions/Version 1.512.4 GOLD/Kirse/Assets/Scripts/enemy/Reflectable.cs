using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflectable : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "slash")
        {
            Vector3 attPos = player.transform.position - transform.position;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            //should go back to turret enemy
            gameObject.GetComponent<Rigidbody2D>().velocity = -attPos.normalized * 8;
        }

    }
}
