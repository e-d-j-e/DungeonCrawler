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
    private float dist = .18f;
    private float speed = 10;
    public GameObject shotPrefab;
    public Vector3 attPos;

    public bool attack;
    public int health = 100;

    public GameObject circuitLootPrefab;
    public GameObject cardLootPrefab;

    bool coroutineStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        attack = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {

        attPos = player.transform.position - transform.position;

        //if()
        //    {
        //    transform.LookAt(player.position);
        //    transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        //}
        //if (.21 > Vector3.Distance(transform.position, player.position) && Vector3.Distance(transform.position, player.position) > -0.21)
        //{
        //    Debug.Log("Enemy hits you! Ouch!");
        //    //TAKE DAMAGE-TAKE KNOCKBACK-UPDATE HEALTH AND SCREEN AND SUCH-STUN ENEMY FOR A SECOND

        //}

        if (gameObject.name == "RoboRange" && attack == true && coroutineStarted == false)
        {
            StartCoroutine("BulletFire");
        }
        if (gameObject.name == "Rock Enemy" && attack == true)
        {
            if (Vector3.Distance(transform.position, player.position) > dist)
            {
                Vector3 theScale = transform.localScale;
                transform.position += attPos.normalized * speed * Time.deltaTime;
                //gameObject.GetComponent<Rigidbody2D>().velocity = attPos.normalized * speed;
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
                //transform.position = (new Vector3(Speed * Time.deltaTime, 0, 0));
            }
        }
        //Enemy Move/Attack list for Ranged

    }
    public IEnumerator BulletFire()
    {
        coroutineStarted = true;
        while (true)
        {

            yield return new WaitForSeconds(2);
            GameObject attack = Instantiate(shotPrefab, transform.position, Quaternion.identity);
            attack.GetComponent<Rigidbody2D>().velocity = attPos.normalized * speed;

            Destroy(attack, 1);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "HitBox")
        {
            player.GetComponent<BasicMovment>().DecreaseHealth(2);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == 9)
        {
            Debug.Log("took damage");
        }
        else if (other.gameObject.tag == "Attack")
        {

            StartCoroutine("SpriteBlink");
            Destroy(other.gameObject);
        }
    }
    IEnumerator SpriteBlink()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            //choose between card or curcuitry upgrade
            int rand = Random.Range(1, 6);
            if (rand == 6)
            {
                Instantiate(circuitLootPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(cardLootPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
