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

    private void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "HitBox")
        {
            Destroy(gameObject);
            player.GetComponent<BasicMovment>().DecreaseHealth(15);
        }

        if (collision.gameObject.tag == "slash")
        {
            Vector3 attPos = player.transform.position - transform.position;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            //should go back to turret enemy
            gameObject.GetComponent<Rigidbody2D>().velocity = -attPos.normalized * 8;
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
