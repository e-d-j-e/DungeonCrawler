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
    private SpriteRenderer spr;
    private float speed = 3;
   
   
    private bool coroutineStarted = false;
    private bool charge;
    private bool SC;
    CardManager cm;
    // Start is called before the first frame update
    void Start()
    {
        cm = CardManager.cm;
        attack = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spr = GetComponent<SpriteRenderer>();
        charge = false;
        SC = true;
        
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
        {//ADD CHARGE MOVEMENT HERE
            if (SC == true)
            {
                StartCoroutine("Charge");
            }
            if (charge == true)
            {

                transform.position += attPos * speed * Time.deltaTime;
                Vector3 theScale = transform.localScale;

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
    public IEnumerator Charge()
    {
        SC = false;
        charge = true;
        yield return new WaitForSeconds(.55f);
        charge = false;
        yield return new WaitForSeconds(1.5f);
        SC = true;
    }
    public IEnumerator BulletFire()
    {
        coroutineStarted = true;
        while (true)
        {
            
            FindObjectOfType<AudioManager>().Play("BMWER");
            yield return new WaitForSeconds(1.3f);
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
            player.GetComponent<BasicMovment>().DecreaseHealth(5);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

       
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
        
    }

    public void takeDamage(int damage)
    {
        StartCoroutine("SpriteBlink");
        ofs = new Vector3(Random.Range(-.6f, 1f), Random.Range(.25f, 1f), 0);
        TextMesh dmgtxt = floatingTextPrefab.GetComponent<TextMesh>();
        dmgtxt.text = damage.ToString();
        GameObject text = Instantiate(floatingTextPrefab, transform.position + ofs, Quaternion.identity);
        Destroy(text, .77f);
        health -= damage;
        if (health <= 20)
        {
            
            spr.color = new Color32(255, 25, 25, 165);
        
        }
        else if (health <= 50)
        {
            spr.color = new Color32(255, 100, 100, 190);
            
        }
        if (health <= 0)
        {
            if(gameObject.name=="Rock")
            {
                
                FindObjectOfType<AudioManager>().Stop("Rocky");
            }
            else if(gameObject.name=="range")
            {
                
                FindObjectOfType<AudioManager>().Stop("BMW");
            }
            Destroy(gameObject);
           
            //choose between card or curcuitry upgrade
            int rand = Random.Range(0, 8);
            if (rand > 5)
            {
                Instantiate(circuitLootPrefab, transform.position, Quaternion.identity);
            }
            else if (rand >= 0)
            {
                //DROPS TWO ITEMS, PICK UP ONE AND ADD TO HAND, DESTROY OTHER CARD
                //ADD way to destroy other gameobject
                Vector2 lootdrop = new Vector2(transform.position.x-3, transform.position.y);               
                GameObject o = (GameObject)Instantiate(BeamLootPrefab, lootdrop, Quaternion.identity);
                cm.lootCount++;
                o.name = cm.lootCount.ToString();
                Card loot;
                loot = cm.lootDeck[cm.loot()];
                o.GetComponent<CardLoot>().LoadCard(loot);

                lootdrop = new Vector2(transform.position.x + 3, transform.position.y);
                GameObject o2 = (GameObject)Instantiate(BeamLootPrefab, lootdrop, Quaternion.identity);
                cm.lootCount++;
                o2.name = cm.lootCount.ToString();
                Card loot2;
                loot2 = cm.lootDeck[cm.loot()];
                o2.GetComponent<CardLoot>().LoadCard(loot2);
            }
            //Player health adjustements
            BasicMovment p = GameObject.FindGameObjectWithTag("Player").GetComponent<BasicMovment>();
            if (p.curHealth <p.maxHealth && p.curHealth >= 90)
            {
                p.curHealth = p.maxHealth;
                float calcHealth = p.curHealth / p.maxHealth;
                p.SetHealthBar(calcHealth);
            }
            else if (p.curHealth < 90)
            {
                p.curHealth += 10;
                float calcHealth = p.curHealth / p.maxHealth;
                p.SetHealthBar(calcHealth);
            }
        }
    }
}

