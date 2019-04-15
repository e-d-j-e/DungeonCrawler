using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject floatingTextPrefab;
    // public BasicMovment test;
    private Transform player;
    private float speed = 1.4f, dist = .18f;
    public GameObject shotPrefab;
    public Vector3 attPos;
    public GameObject cardlootprefab;
    public GameObject curcuitlootprefab;

    public bool attack;
    public int health = 100;

    private Vector3 ofs; 
    // Start is called before the first frame update
    void Start()
    {
        attack = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (gameObject.name == "RoboRange")
        {
            StartCoroutine("BulletFire");
        }
    }

    // Update is called once per frame
    void Update()
    {

        attPos = player.transform.position - transform.position;


        if (gameObject.name == "Rock Enemy" && attack == true)
        {
            if (Vector3.Distance(transform.position, player.position) > dist)
            {
                Vector3 theScale = transform.localScale;
                transform.position += attPos * speed * Time.deltaTime;
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
        while (true)
        {

            yield return new WaitForSeconds(2);
            GameObject attack = Instantiate(shotPrefab, transform.position, Quaternion.identity);
            attack.GetComponent<Rigidbody2D>().velocity = attPos * 1.5f;
            
            //if hits player, destroy
            Destroy(attack, 3);
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
        Destroy(gameObject);
    }
    public void takeDamage(int damage)
    {
        ofs = new Vector3(Random.Range(-.6f, .6f), Random.Range(0, .5f), 0);
        TextMesh dmgtxt = floatingTextPrefab.GetComponent<TextMesh>();
        dmgtxt.text = damage.ToString();
        GameObject text= Instantiate(floatingTextPrefab, transform.position+ofs, Quaternion.identity);
        //AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(text, .55f);
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            //choose between card or curcuitry upgrade
            //TODO
            //change to foor loop for beta test
            int rand = Random.Range(1,6);
            if(rand>4)
            {
                Instantiate(curcuitlootprefab, transform.position, Quaternion.identity);
            }
            else 
            {
                Instantiate(cardlootprefab, transform.position, Quaternion.identity);
            }
        }
    }

   
    

}
