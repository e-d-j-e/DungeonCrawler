using System.Collections;
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
//
//


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
    public Animator animator;

    float dashDistance = 2.5f;

    private Vector2 movement;
    public float movSpd = 8f;
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
    public bool attacked = false;

    public LayerMask whatIsEnemies;
    public Transform attackPos;
    public float attackRange;
    public bool dashAttack = false;

    [HideInInspector]
    public Vector3 UIoffset;
    [HideInInspector]
    public Vector3 camoffset;
    [HideInInspector]
    public Vector3 move;
    [HideInInspector]
    public Vector2 point;

    // Start is called before the first frame update
    void Start()
    {
        float calcHealth = curHealth / maxHealth;
        SetHealthBar(calcHealth);
        spriteR = GetComponentInChildren<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>(spriteNames);
        camoffset = cam.transform.position;
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

        }
    }
    private void movesound()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            FindObjectOfType<AudioManager>().Play("playerWalk");
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            FindObjectOfType<AudioManager>().Stop("playerWalk");

    }
    public IEnumerator AttackRelease(float t)
    {
        //INSERT SLOW/STOP PLAYER MESS AROUND WITH NUMBERS
        movSpd = .25F;
        yield return new WaitForSeconds(.4f);
        movSpd = 8f;
        yield return new WaitForSeconds(t);
        attacked = false;
        
    }




    //Crosshair follows mouse movements relative to players position
    private void aimCrosshair()
    {
        Vector3 aim = Input.mousePosition;
        aim = Camera.main.ScreenToWorldPoint(aim);
        Vector2 mouse = new Vector2(aim.x - transform.position.x, aim.y - transform.position.y);
        Vector2 direction = new Vector2(aim.x - transform.position.x, aim.y - transform.position.y);


        if (mouse.magnitude > 0)
        {
            mouse.Normalize();
            mouse *= 1.12f;
            crosshair.transform.localPosition = mouse;
            //if (crshrKey == false)
            //{
            //    crshrKey = true;
            //}
            direction.Normalize();
            

            //HOKUS-POKE-US
            if (Input.GetKeyDown(KeyCode.Mouse0) && attacked == false)
            {
                GameObject attack = Instantiate(hocusPokeusPrefab, transform.position, Quaternion.identity);
                attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                Destroy(attack, 0.4f);
                attacked = true;
                StartCoroutine(AttackRelease(.38f));
                //isMove = true;
            }
        }
    }

    IEnumerator SpriteBlink()
    {
        cam.GetComponent<ScreenShake>().TriggerShake();

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

        yield return new WaitForSeconds(2f);
    }



    //public void DamageCalculator()
    //{
    //    float damageMultiplier = 1;
    //    //curHealth -= maxHealth * damageMultiplier;
    //}

    public void SetHealthBar(float f)
    {
        healthBar.transform.localScale = new Vector3(f, 1, 1);
    }
    
    public void Slash()
    {
        GameObject attack = Instantiate(slashPrefab, transform.position, Quaternion.identity);
        attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(attack, 0.51f);
    }
    public void Beam()
    {
        GameObject attack = Instantiate(beamPrefab, transform.position, Quaternion.identity);
        attack.GetComponent<Rigidbody2D>().velocity = direction.normalized * 22f;
        attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(attack, 2);
    }
    public void Dash()
    {
        hitBox.tag = "Dash";
        animator.Play("Dash");
        //transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        FindObjectOfType<AudioManager>().Play("Dash");
        StartCoroutine(DashAtt(direction, coll));

    }

    public void BoomerangDash()
    {
        hitBox.tag = "Dash";
        animator.Play("Dash");
        //transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        FindObjectOfType<AudioManager>().Play("Dash");
        StartCoroutine(BoomerangDashCo(direction, coll));

    }
    public void SuperBeam()
    {
        GameObject attack = Instantiate(superbeamPrefab, transform.position, Quaternion.identity);
        attack.GetComponent<Rigidbody2D>().velocity = direction.normalized * 25f;
        attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(attack, 2f);
    }
    public void SpinSlash()
    {
        StartCoroutine("SpinSlashAttack");
        GameObject attack = Instantiate(spinslashPrefab, transform.position, Quaternion.identity);
        attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(attack, .6f);
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
   
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public IEnumerator SpinSlashAttack()
    {
        AttackRange(2);

        yield return new WaitForSeconds(.3F);
        AttackRange(2);
        yield return new WaitForSeconds(.3F);
        AttackRange(2);
        yield break;
    }

    public void AttackRange(float f)
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, f, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (enemiesToDamage[i].GetComponentInChildren<enemyAI>() == null) { }
            Debug.Log("Enemy" + i);
            enemiesToDamage[i].GetComponentInChildren<enemyAI>().takeDamage(25);

        }
    }
    IEnumerator DashAtt(Vector3 direction, Collider2D coll)
    {
        transform.GetChild(0).Rotate(0, 0, (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        GetComponent<Rigidbody2D>().velocity = direction.normalized * 25;
        gameObject.layer = 9; //Dash layer
        hitBox.tag = "Dash";
        yield return new WaitForSeconds(.3f);
        transform.GetChild(0).Rotate(0, 0, -(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        gameObject.layer = 8; //Player 

        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        spriteVersion = 1;
        spriteVersion = 0;
        hitBox.tag = "Hitbox";
        dashAttack = false;
    }
    public IEnumerator BoomerangDashCo(Vector3 direction, Collider2D coll)
    {
        transform.GetChild(0).Rotate(0, 0, (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        GetComponent<Rigidbody2D>().velocity = direction.normalized * 50;
        gameObject.layer = 9; //Dash layer
        hitBox.tag = "Dash";
        yield return new WaitForSeconds(.2f);
        animator.Play("Dash");
        transform.GetChild(0).Rotate(0, 0, -(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
        GetComponent<Rigidbody2D>().velocity = -direction.normalized * 50;
        yield return new WaitForSeconds(.2f);
        gameObject.layer = 8; //Player 

        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        spriteVersion = 1;
        spriteVersion = 0;
        hitBox.tag = "Hitbox";
        dashAttack = false;
    }

}