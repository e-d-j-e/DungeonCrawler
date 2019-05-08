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

    public GameObject chargeProjectile;
    public Transform[] teleSpot;
    public bool attack;

    public int health = 100;

    private Transform player;
    private Vector3 ofs;
    private SpriteRenderer spr;
    private float speed = 15f;


    private bool coroutineStarted = false;
    private bool charge;
    private bool SC;
    private bool stun;
    CardManager cm;
    public GameObject shld;
    Color32 b = new Color32(166, 166, 166, 210);

    bool canHit = true;
    public bool canDash = true;
    public bool hittable = false;

    public bool turretNotCharge = false;

    public float armorAmount;
    int destPoint = 0;
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
        if(this.gameObject.name == "asd")
        {
            armorAmount = .6f;
        }
        else { armorAmount = 1; }
    }

    // Update is called once per frame
    void Update()
    {

        attPos = player.transform.position - transform.position;
        if (gameObject.name == "Turret")
        {
            turretNotCharge = false;
            if(attack == true)
            {
                StartCoroutine("TurretCharge");
                
            }
        }

        if ((gameObject.name == "range" || gameObject.name == "range2") && attack == true && coroutineStarted == false)
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
                transform.position += attPos.normalized * speed * Time.deltaTime;
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
        if (stun==true)
        {
            shld.SetActive(false);
            spr.color = new Color32(130, 130, 69, 255);
        }
        SC = true;


    }
    public IEnumerator BulletFire()
    {
        coroutineStarted = true;
        while (true)
        {

            //FindObjectOfType<AudioManager>().Play("BMWER");
            yield return new WaitForSeconds(1.3f);
            //FindObjectOfType<AudioManager>().Stop("BMWER");

            GameObject attack = Instantiate(shotPrefab, transform.position, Quaternion.identity);
            attack.GetComponent<Rigidbody2D>().velocity = attPos.normalized * speed;

            Destroy(attack, 6);
            yield return new WaitForSeconds(.6F);
        }
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
            else if (other.gameObject.tag == "slash"|| other.gameObject.tag == "spinslash")
            {
                StartCoroutine(Stun());
                stun = true;
            }            
            
        }
        else if (charge==true)
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
        
        speed = 0;
        spr.color = new Color32(130, 130, 69, 255);
        yield return new WaitForSeconds(2f);
        spr.color = Color.white;
        speed = 15f;
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
        float armoredUnit = damage * armorAmount;//for tanky monster

        dmgtxt.text = armoredUnit.ToString();
        GameObject text = Instantiate(floatingTextPrefab, transform.position + ofs, Quaternion.identity);
        Destroy(text, .77f);
       
        health -= Mathf.RoundToInt(armoredUnit);

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
                Vector2 lootdrop = new Vector2(transform.position.x - 3, transform.position.y);
                GameObject o = Instantiate(BeamLootPrefab, lootdrop, Quaternion.identity);
                cm.lootCount++;
                o.name = cm.lootCount.ToString();
                Card loot;
                loot = cm.lootDeck[cm.loot()];
                o.GetComponent<CardLoot>().LoadCard(loot);

                lootdrop = new Vector2(transform.position.x + 3, transform.position.y);
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
    IEnumerator TurretCharge()
    {
        while (true)
        {
            attack = false;
            turretNotCharge = false;
            //animation
            yield return new WaitForSeconds(3);
            //Fire Code
            FireChargeShot();
            turretNotCharge = true;
            //teleport
            GotoNextPoint();
        }
    }
    void GotoNextPoint()
    {
        
        
        // Returns if no points have been set up
        if (teleSpot.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        transform.position  = teleSpot[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % teleSpot.Length;
    }
    void FireChargeShot()
    {
        GameObject o = Instantiate(chargeProjectile, transform.position, Quaternion.identity);
        o.GetComponent<Rigidbody2D>().velocity = attPos.normalized * 50;
        Destroy(o,3);
    }

}

