﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Ethan 
//March 28, 2019
//Version 0.25.00
//Desc: Basic Player movement script for the character player. Player has crosshair, WASD movement keys, 
//    Poke-Space Dash-Capslock Slash-Q Beam-E 
//    crosshair follows mouse around singular point, attacks and animations will follow this crosshair position
//
//    any tested animation stuff is commented out. 
//New on new version:
//0.25.00- added animations, dash broke tho rip
//
//





//TODO:
/*4 key inputs
 *hocus poke-us attack input
 * 
 * 
 */


public class BasicMovment : MonoBehaviour
{

    public BoxCollider2D hitBox;
    public GameObject hocusPokeusPrefab;
    public GameObject slashPrefab;
    //public GameObject dashPrefab;
    public GameObject beamPrefab;
    public GameObject spinslashPrefab;
    public GameObject superbeamPrefab;
    public GameObject crosshair;
    public GameObject cam;
    public GameObject UI;
    public Animator animator;

    public Vector3 UIoffset;
    public Vector3 camoffset;
    public Vector3 move;
    public Vector2 point;

    float dashDistance = 2.5f;

    private Vector2 movement;
    public float movSpd = 2.9f;
    public float curHealth = 100;
    public float maxHealth = 100;
    public GameObject healthBar;

    public bool dmgPossible;

    //SPRITE VARIABLES
    private string spriteNames = "dash";
    private int spriteVersion = 0;
    public SpriteRenderer spriteR;
    private Sprite[] sprites;
    Collider2D coll;

    private short count = 0;
    private bool crshrKey = false;


    //Testing Aim Variables
    Vector3 aim;
    Vector2 mouse;
    Vector2 direction;
    bool attacked = false;

    public LayerMask whatIsEnemies;
    public Transform attackPos;
    public float attackRange;


    public bool dashAttack = false;
    public float timeBtwAttack = 0;
    public float startTimeBtwAttack;

    // Start is called before the first frame update
    void Start()
    {
        startTimeBtwAttack = .01f;
        spriteR = GetComponentInChildren<SpriteRenderer>();
        //spriteR = GetGetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>(spriteNames);
        camoffset = cam.transform.position;
        //UIoffset = UI.transform.position - transform.position;
        //SetHealthBar(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            //Aiming
            aim = Input.mousePosition;
            aim = Camera.main.ScreenToWorldPoint(aim);
            mouse = new Vector2(aim.x - transform.position.x, aim.y - transform.position.y);
            direction = new Vector2(aim.x - transform.position.x, aim.y - transform.position.y);
            //movement for player
            move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

            transform.position += ((movSpd * move) * Time.deltaTime);

            //Animations
            animator.SetFloat("Horizontal", move.x);
            animator.SetFloat("Vertical", move.y);
            animator.SetFloat("Magnitude", move.magnitude);
            //sound
            movesound();

            aimCrosshair();
            if(timeBtwAttack <= 0 && dashAttack == true)
            {
                Collider2D enemy = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsEnemies);
                enemy.GetComponentInChildren<enemyAI>().takeDamage(20);
                //Collider2D[] enemiesToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, whatIsEnemies);
                //for (int i = 0; i < enemiesToDamage.Length; i++)
                //{
                //    Debug.Log("Enemy" + i);
                //    enemiesToDamage[i].GetComponentInChildren<enemyAI>().takeDamage(20);
                //}
            }
            else { timeBtwAttack -= Time.deltaTime; }

        }
    }
    private void movesound()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            FindObjectOfType<AudioManager>().Play("playerWalk");
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            FindObjectOfType<AudioManager>().Stop("playerWalk");

    }


    IEnumerator Example(Vector3 direction, Collider2D coll)
    {
        dashAttack = true;

        transform.GetChild(0).Rotate(0, 0, (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
        hitBox.gameObject.SetActive(false);
        GetComponent<Rigidbody2D>().velocity = direction.normalized * 25;
        gameObject.layer = 9; //Dash layer
        hitBox.tag = "Dash";
        yield return new WaitForSeconds(.14f);
        transform.GetChild(0).Rotate(0, 0, -(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg)); 
        hitBox.gameObject.SetActive(true);
        gameObject.layer = 8; //Player 
        
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        spriteVersion = 1;
        spriteVersion = 0;
        hitBox.tag = "Hitbox";
        dashAttack = false;
    }

    //Crosshair follows mouse movements relative to players position
    private void aimCrosshair()
    {
        Vector3 aim = Input.mousePosition;
        aim = Camera.main.ScreenToWorldPoint(aim);
        Vector2 mouse = new Vector2(aim.x - transform.position.x, aim.y - transform.position.y);
        Vector2 direction = new Vector2(aim.x - transform.position.x, aim.y - transform.position.y);

        if (count == 0)
        {
            //crosshair.SetActive(true);
            crshrKey = true;
            count = 1;

        }

        if (mouse.magnitude > 0)
        {
            mouse.Normalize();
            mouse *= 1.12f;
            crosshair.transform.localPosition = mouse;
            //if (crshrKey == false)
            //{
            //    //crosshair.SetActive(true);
            //    crshrKey = true;


            //}


            direction.Normalize();

            //Input keys for activating cards

            //HOKUS-POKE-US
            if (Input.GetKeyDown(KeyCode.Mouse0) && attacked == false)
            {
                //useCard();
               
                GameObject attack = Instantiate(hocusPokeusPrefab, transform.position, Quaternion.identity);
                attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                Destroy(attack, 0.4f);
                attacked = true;
                Invoke("AttackRelease", .5f);
                //isMove = true;
            }
        }
    }

    IEnumerator SpriteBlink()
    {
        
        cam.GetComponent<ScreenShake>().TriggerShake();
        hitBox.enabled = false;
        spriteR.enabled = false;
        //cam.transform.position += Vector3.right;
        yield return new WaitForSeconds(.1f);
        spriteR.enabled = true;
        //cam.transform.position += Vector3.left;
        yield return new WaitForSeconds(.1f);
        spriteR.enabled = false;
        yield return new WaitForSeconds(.1f);
        spriteR.enabled = true;
        yield return new WaitForSeconds(1f);
        hitBox.enabled = true;
  
        yield return new WaitForSeconds(2f);
    }



    public void DamageCalculator()
    {
        float damageMultiplier = 1;
        //curHealth -= maxHealth * damageMultiplier;
    }
    public void SetHealthBar(float f)
    {
        healthBar.transform.localScale = new Vector3(f, 1, 1);
    }

    public void Slash()
    {
        GameObject attack = Instantiate(slashPrefab, transform.position, Quaternion.identity);
        attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(attack, 0.5f);
    }
    public void Beam()
    {
        GameObject attack = Instantiate(beamPrefab, transform.position, Quaternion.identity);
        attack.GetComponent<Rigidbody2D>().velocity = direction * 3.5f;
        attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(attack, 2);
    }
    public void Dash()
    {
        
        animator.Play("Dash");
        timeBtwAttack = startTimeBtwAttack;
        //transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        FindObjectOfType<AudioManager>().Play("Dash");
        StartCoroutine(Example(direction, coll));

    }
    public void SuperBeam()
    {
        GameObject attack = Instantiate(superbeamPrefab, transform.position, Quaternion.identity);
        attack.GetComponent<Rigidbody2D>().velocity = direction * 3.5f;
        attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(attack, 2.5f);
    }
    public void SpinSlash()
    {
        GameObject attack = Instantiate(spinslashPrefab, transform.position, Quaternion.identity);
        attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(attack, 1f);
    }

    public void DecreaseHealth(float f)
    {
        curHealth -= f;
        StartCoroutine("SpriteBlink");
        FindObjectOfType<AudioManager>().Play("OUCH");
        float calcHealth = curHealth / maxHealth;
        SetHealthBar(calcHealth);
        if (curHealth <= 0)
        {
            curHealth = 0;
            SceneManager.LoadScene(0);
        }

    }
    void AttackRelease()
    {
        attacked = false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}