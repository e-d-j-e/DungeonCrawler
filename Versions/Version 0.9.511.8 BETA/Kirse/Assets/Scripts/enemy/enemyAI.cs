﻿using System.Collections;
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
    //for charge beam shot
    public GameObject chargeShot;
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
    //movement speed
    private float rockspeed = 15f;
    Vector3 rangePos;

    //add new shooting speed
    float shootspeed = 10f;

    private float speed;



    private bool coroutineStarted = false;
    private bool charge;
    private bool SC;
    private bool stun;
    int chrgchnce = 0;
    CardManager cm;
    public GameObject shld;
    Color32 b = new Color32(166, 166, 166, 210);

    bool canHit = true;
    public bool canDash = true;
    public bool hittable = false;
    // Start is called before the first frame update
    void Start()
    {
        cm = CardManager.cm;
        attack = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spr = GetComponent<SpriteRenderer>();
        charge = false;
        SC = true;
        stun = false;
        rangePos = player.transform.position - transform.position;
        speed = 3f;

    }

    // Update is called once per frame
    void Update()
    {

        if (this.name == "range" && attack == true)
        {
            rangePos = player.transform.position - transform.position;
            if (Vector3.Distance(player.transform.position, transform.position) < 8)
            {
                transform.position += -rangePos.normalized * speed * Time.deltaTime;
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
            if (coroutineStarted == false)
            {
                chrgchnce = Random.Range(0, 7);
                if (chrgchnce > 5)
                {
                    StartCoroutine("ChargedShot");
                }
                else if (chrgchnce >= 0)
                {
                    StartCoroutine("BulletFire");

                }
            }


        }
        if (this.name == "Rock" && attack == true)
        {//ADD CHARGE MOVEMENT HERE
            if (SC == true)
            {
                StartCoroutine("Charge");
            }
            if (charge == true)
            {
                transform.position += attPos.normalized * rockspeed * Time.deltaTime;
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

    public IEnumerator Charge()
    {
        SC = false;
        if (stun == false)
        {
            shld.SetActive(true);
            yield return new WaitForSeconds(.12f);
            spr.color = b;
            yield return new WaitForSeconds(.12f);
            spr.color = Color.white;
        }
        if (stun == false)
        {
            yield return new WaitForSeconds(.12f);
            spr.color = b;
            yield return new WaitForSeconds(.12f);
            spr.color = Color.white;
        }
        if (stun == false)
        {
            yield return new WaitForSeconds(.12f);
            spr.color = b;
            yield return new WaitForSeconds(.12f);
            spr.color = Color.white;
        }
        if (stun == false)
        {
            yield return new WaitForSeconds(.12f);
            spr.color = b;
            yield return new WaitForSeconds(.12f);
            spr.color = Color.white;
        }
        if (stun == false)
        {
            yield return new WaitForSeconds(.12f);
            spr.color = b;
            yield return new WaitForSeconds(.12f);
            spr.color = Color.white;
        }
        if (stun == false)
        {
            shld.SetActive(false);
            attPos = player.transform.position - transform.position;
            charge = true;
            yield return new WaitForSeconds(0.47f);
            charge = false;

        }
        if (stun == true)
        {
            shld.SetActive(false);
            spr.color = new Color32(130, 130, 69, 255);
        }
        SC = true;


    }
    public IEnumerator BulletFire()
    {
        coroutineStarted = true;

        yield return new WaitForSeconds(1.3f);
        attPos = player.transform.position - transform.position;
        GameObject attack = Instantiate(shotPrefab, transform.position, Quaternion.identity);
        attack.GetComponent<Rigidbody2D>().velocity = attPos.normalized * shootspeed;
        Destroy(attack, 5);
        yield return new WaitForSeconds(.6f);


        coroutineStarted = false;

    }

    IEnumerator ChargedShot()
    {
        coroutineStarted = true;
        speed = 0;
        //charge shot animation
        spr.color = new Color32(230, 230, 230, 255);
        yield return new WaitForSeconds(1.5f);
        spr.color = new Color32(160, 160, 160, 255);
        yield return new WaitForSeconds(1.5f);
        spr.color = new Color32(100, 100, 100, 255);
        yield return new WaitForSeconds(1.5f);
        spr.color = Color.white;
        attPos = player.transform.position - transform.position;
        GameObject att = Instantiate(chargeShot, transform.position, Quaternion.identity);
        att.GetComponent<Rigidbody2D>().velocity = attPos.normalized * shootspeed;
        Destroy(att, 4);
        yield return new WaitForSeconds(.4f);
        speed = 3f;
        coroutineStarted = false;
    }

    private void CanHitReset() { canHit = true; canDash = true; }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (charge == false)
        {
            if (other.gameObject.tag == "Hitbox" && canHit == true)
            {
                player.GetComponent<BasicMovment>().DecreaseHealth(5);
                canHit = false;
                Invoke("CanHitReset", 1.5f);
            }
            if (other.gameObject.tag == "Dash" && canDash == true)
            {
                StartCoroutine(Stun());
                stun = true;
            }
            else if (other.gameObject.tag == "slash" || other.gameObject.tag == "spinslash")
            {
                StartCoroutine(Stun());
                stun = true;
            }

        }
        else if (charge == true)
        {
            if (other.gameObject.tag == "Hitbox" && canHit == true)
            {
                player.GetComponent<BasicMovment>().DecreaseHealth(5);
                canHit = false;
                Invoke("CanHitReset", 1.5f);
            }
            if (other.gameObject.tag == "Dash" && canDash == true)
            {
                takeDamage(8);
                //canDash = false;
                Invoke("CanHitReset", 1);
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D other)
    {       //we are able to use the tag "Attack" instead of beam  and use cardtypes to determine certain functions
        if (other.gameObject.tag == "beam" && cm.usedCardType is CardTypeBeam && charge == false)
        {
            //Debug.Log("Heal");
            //Heal(65);
            //spr.color = new Color(255, 255, 255);
            //if(other.gameObject.GetComponent)
            //StartCoroutine("SpriteBlink");
            Destroy(other.gameObject);
        }

    }
    IEnumerator Stun()
    {

        rockspeed = 0;
        spr.color = new Color32(130, 130, 69, 255);
        yield return new WaitForSeconds(2f);
        spr.color = Color.white;
        rockspeed = 15f;
        stun = false;
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
    public void Heal(int i)
    {
        ofs = new Vector3(Random.Range(-.6f, 1f), Random.Range(.25f, 1f), 0);
        TextMesh dmgtxt = floatingTextPrefab.GetComponent<TextMesh>();
        dmgtxt.color = Color.green;

        dmgtxt.text = i.ToString();
        GameObject text = Instantiate(floatingTextPrefab, transform.position + ofs, Quaternion.identity);

        Destroy(text, 2);
    }
    public void takeDamage(int damage)
    {
        StartCoroutine("SpriteBlink");
        ofs = new Vector3(Random.Range(-.6f, 1f), Random.Range(.25f, 1f), 0);
        TextMesh dmgtxt = floatingTextPrefab.GetComponent<TextMesh>();
        dmgtxt.color = Color.white;

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
            if (gameObject.name == "Rock")
            {

                FindObjectOfType<AudioManager>().Stop("Rocky");
            }
            else if (gameObject.name == "range")
            {

                FindObjectOfType<AudioManager>().Stop("BMW");
            }
            Destroy(transform.parent.gameObject);
            DestroyableWall.KilledEnemies();
            //FOR NOW MONSTERS ONLY WILL DROP CARDS. THE MOD LOOT WILL COME FROM THE ONE CHEST TO SHOW OFF THAT


            //choose between card or curcuitry upgrade
            int rand = 1;
            //Random.Range(0, 8);
            //if (rand > 5)
            //{
            //    Instantiate(circuitLootPrefab, transform.position, Quaternion.identity);
            //}
            //else
            if (rand >= 0)
            {
                //DROPS TWO ITEMS, PICK UP ONE AND ADD TO HAND, DESTROY OTHER CARD
                //ADD way to destroy other gameobject
                //Vector2 lootdrop = new Vector2(transform.position.x - 3, transform.position.y);
                Vector2 lootdrop = new Vector2(player.transform.position.x - 3, player.transform.position.y);
                GameObject o = Instantiate(BeamLootPrefab, lootdrop, Quaternion.identity);
                cm.lootCount++;
                o.name = cm.lootCount.ToString();
                Card loot;
                loot = cm.lootDeck[cm.loot()];
                o.GetComponent<CardLoot>().LoadCard(loot);

                lootdrop = new Vector2(player.transform.position.x + 3, player.transform.position.y);
                GameObject o2 = Instantiate(BeamLootPrefab, lootdrop, Quaternion.identity);
                cm.lootCount++;
                o2.name = cm.lootCount.ToString();
                Card loot2;
                loot2 = cm.lootDeck[cm.loot()];
                o2.GetComponent<CardLoot>().LoadCard(loot2);
            }
            //Player health adjustements
            BasicMovment p = GameObject.FindGameObjectWithTag("Player").GetComponent<BasicMovment>();
            if (p.curHealth < p.maxHealth && p.curHealth >= 90)
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

