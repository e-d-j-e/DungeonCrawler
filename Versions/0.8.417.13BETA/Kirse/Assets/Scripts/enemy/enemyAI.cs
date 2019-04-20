using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Version #.#.MD.h

//Ethan 
//April 6, 2019
//Version 0.5.416.0
//Desc: Basic Enemy AI, follows player to real close. 
//New on new version:
//0.15.328- added new collisions 
//0.5.416.0- enemies drop card, mods, shows damage text
//
public class enemyAI : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    public GameObject shotPrefab;
    public Vector3 attPos;
    public GameObject circuitLootPrefab;
    public GameObject BeamLootPrefab;
    public GameObject SlashLootPrefab;
    public GameObject DashLootPrefab;
    public bool attack;

    public int health = 100;

    private Transform player;
    private Vector3 ofs;
    private float dist = .18f;
    private float speed = 6;

    private bool coroutineStarted = false;
    CardManager cm;
    // Start is called before the first frame update
    void Start()
    {
        cm = CardManager.cm;
        attack = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {

        attPos = player.transform.position - transform.position;


        if (gameObject.name == "range" && attack == true && coroutineStarted == false)
        {
            
            StartCoroutine("BulletFire");
        }
        if (gameObject.name == "Rock" && attack == true)
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

            }
        }
        //Enemy Move/Attack list for Ranged

    }
    public IEnumerator BulletFire()
    {
        coroutineStarted = true;
        while (true)
        {
            
            FindObjectOfType<AudioManager>().Play("BMWER");
            yield return new WaitForSeconds(2);
            FindObjectOfType<AudioManager>().Stop("BMWER");
          
            GameObject attack = Instantiate(shotPrefab, transform.position, Quaternion.identity);
            attack.GetComponent<Rigidbody2D>().velocity = attPos.normalized * speed;

            Destroy(attack, 3);
            yield return new WaitForSeconds(.6F);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hitbox")
        {
            player.GetComponent<BasicMovment>().DecreaseHealth(2);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

       if(other.gameObject.tag == "Dash")
        {
            StartCoroutine("SpriteBlink");
            takeDamage(30);
        }
       if (other.gameObject.tag == "Attack")
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
        //Destroy(gameObject);
    }

    public void takeDamage(int damage)
    {
        ofs = new Vector3(Random.Range(-.6f, .6f), Random.Range(0, .5f), 0);
        TextMesh dmgtxt = floatingTextPrefab.GetComponent<TextMesh>();
        dmgtxt.text = damage.ToString();
        GameObject text = Instantiate(floatingTextPrefab, transform.position + ofs, Quaternion.identity);
        Destroy(text, .77f);
        health -= damage;
        if (health <= 0)
        {
            if(gameObject.name=="Rock")
            {
                //GameObject go = GameObject.FindGameObjectWithTag("Enemy");
                //Destroy(go);
                FindObjectOfType<AudioManager>().Stop("Rocky");
            }
            else if(gameObject.name=="range")
            {
                //GameObject go = GameObject.FindGameObjectWithTag("EnemyM");
                //Destroy(go);
                FindObjectOfType<AudioManager>().Stop("BMW");
            }
            Destroy(gameObject);
           
           

            FindObjectOfType<AudioManager>().Stop("Rocky");
            //choose between card or curcuitry upgrade
            int rand = Random.Range(0, 8);
            if (rand > 5)
            {
                Instantiate(circuitLootPrefab, transform.position, Quaternion.identity);
            }
            else if (rand >= 0)
            {
                GameObject o = Instantiate(BeamLootPrefab, transform.position, Quaternion.identity);
                Card loot;
                loot = cm.lootDeck[cm.loot()];
                o.GetComponent<CardLoot>().LoadCard(loot);
            }
            //else if (rand >= 2)
            //{
            //    Instantiate(DashLootPrefab, transform.position, Quaternion.identity);
            //}
            //else if (rand >= 0)
            //{
            //    Instantiate(SlashLootPrefab, transform.position, Quaternion.identity);
            //}
        }
    }
}

