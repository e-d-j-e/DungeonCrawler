using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject crosshair;
    public GameObject cam;
    public GameObject UI;
    public Animator animator;

    public Vector3 UIoffset;
    public Vector3 camoffset;
    public Vector3 move;
    public Vector2 point;
    public Vector2 speed = new Vector2(2, 2);
    public Vector2 relativePosition;
    private Vector2 movement;
    private float movSpd = 2.9f;
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


    // Start is called before the first frame update
    void Start()
    {
       
            spriteR = GetComponentInChildren<SpriteRenderer>();
            //spriteR = GetGetComponent<SpriteRenderer>();
            sprites = Resources.LoadAll<Sprite>(spriteNames);
            camoffset = cam.transform.position ;
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

            animator.SetFloat("Horizontal", move.x);
            animator.SetFloat("Vertical", move.y);
            animator.SetFloat("Magnitude", move.magnitude);


            aimCrosshair();


        }
    }



    IEnumerator Example(Vector3 direction, Collider2D coll)
    {
        gameObject.layer = 11; //Dash layer
        //spriteR.sprite = sprites[spriteVersion];
        yield return new WaitForSeconds(.14f);
        gameObject.layer = 8; //Player 
        //gameObject.transform.position = Vector3.zero;
        GetComponent<Rigidbody2D>().velocity = direction*0;
        spriteVersion = 1;
        //spriteR.sprite = sprites[spriteVersion];
        //transform.Rotate(0, 0, -(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
        spriteVersion = 0;
        //coll.isTrigger = false;
        crshrKey = false;

    }

    //Crosshair follows mouse movements relative to players position
    private void aimCrosshair()
    {
        Vector3 aim = Input.mousePosition;
        aim = Camera.main.ScreenToWorldPoint(aim);
        Vector2 mouse = new Vector2(aim.x - transform.position.x, aim.y - transform.position.y);
        Vector2 direction = new Vector2(aim.x - transform.position.x, aim.y - transform.position.y);
        
        if(count==0)
            {
            //crosshair.SetActive(true);
            crshrKey = true;
            count=1;
           
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
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //useCard();
                Vector3 temp;
                temp = new Vector3(transform.position.x+1, transform.position.y+.5f, 0);
                GameObject attack = Instantiate(hocusPokeusPrefab, temp, Quaternion.identity);
                attack.GetComponent<Rigidbody2D>().velocity = direction;
                attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                Destroy(attack, 0.26f);
                //isMove = true;
            }
        }
    }       
    
    IEnumerator SpriteBlink()
    {
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

    public void DecreaseHealth(float f)
    {
        curHealth -= f;
        StartCoroutine("SpriteBlink");
        float calcHealth = curHealth / maxHealth;
        SetHealthBar(calcHealth);
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
        attack.GetComponent<Rigidbody2D>().velocity = direction * 1.5f;
        attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Destroy(attack, 0.32F);
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
        
        GetComponent<Rigidbody2D>().velocity = direction.normalized * 15;
        //transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        StartCoroutine(Example(direction, coll));

    }
  

}