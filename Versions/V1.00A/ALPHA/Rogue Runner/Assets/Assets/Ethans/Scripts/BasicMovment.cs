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
   
    public GameObject hocusPokeusPrefab;
    public GameObject slashPrefab;
    //public GameObject dashPrefab;
    public GameObject beamPrefab;
    public GameObject crosshair;
    public GameObject cam;
    public GameObject UI;
    public Animator animator;

    private Vector3 UIoffset;
    private Vector3 camoffset;
    private Vector3 move;
    private Vector2 point;
    private Vector2 speed = new Vector2(2, 2);
    private Vector2 relativePosition;
    private Vector2 movement;
    private float movSpd = 2.9f;
    public float curHealth = 100;
    public float maxHealth = 100;
    public GameObject healthBar;

    private float topVarY = 5;
    private float botVarY = -5;
    private float leftVarX = -5;
    private float rightVarX = 5;
    



    //SPRITE VARIABLE
    private byte spriteVersion = 0;
    private byte beamVersion = 0;
    private SpriteRenderer spriteR;
    private SpriteRenderer beamSR;
    private Sprite[] beamS;
    private Sprite[] sprites;
    Collider2D coll;

    private short count = 0;
    private bool crshrKey = false;

    // Start is called before the first frame update
    void Start()
    {    
           
            spriteR = GetComponent<SpriteRenderer>();
            
            sprites = Resources.LoadAll<Sprite>("dash");
           
            camoffset = cam.transform.position - transform.position;
            UIoffset = UI.transform.position - transform.position;
        //SetHealthBar(maxHealth);
        animator.SetBool("isBeam", false);
        animator.SetBool("isBasic", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //movement for player
        move= new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0);
        transform.position+= ((movSpd*move)*Time.deltaTime);
       
        cam.transform.position = transform.position + camoffset;
        UI.transform.position = transform.position +UIoffset;



        //animations for player
        animator.SetFloat("Horizontal", move.x);
        animator.SetFloat("Vertical", move.y);
        animator.SetFloat("Magnitude", move.magnitude);
       
        aimCrosshair();

    }
    //IEnumerator Beam(GameObject att)
    //{
    //    beamSR = beamPrefab.GetComponent<SpriteRenderer>();
    //    beamS = Resources.LoadAll<Sprite>("beam");
    //    //switch between the 4 sprites
    //    Debug.Log("1");
    //    beamSR.sprite=beamS[beamVersion];
    //    yield return new WaitForSeconds(.2f);
    //    beamVersion++;
    //    Debug.Log("2");
    //    beamSR.sprite = beamS[beamVersion];
    //    yield return new WaitForSeconds(.2f);
    //    beamVersion++;
    //    Debug.Log("3");
    //    beamSR.sprite = beamS[beamVersion];
    //    yield return new WaitForSeconds(.2f);
    //    beamVersion++;
    //    Debug.Log("4");
    //    beamSR.sprite = beamS[beamVersion];
    //    yield return new WaitForSeconds(.2f);
    //    beamVersion = 0;
    //    //StartCoroutine(Beam(att));
    //}


    IEnumerator Dash(Vector3 direction, Collider2D coll)
    {
        
        spriteR.sprite = sprites[spriteVersion];
        yield return new WaitForSeconds(.14f);
        
        GetComponent<Rigidbody2D>().velocity = direction * 0;
        spriteVersion = 1;
        spriteR.sprite = sprites[spriteVersion];
        //transform.Rotate(0, 0, -(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
        spriteVersion = 0;
        coll.isTrigger = false;
        crshrKey = false;

    }

    //Crosshair follows mouse movements relative to players position
    private void aimCrosshair()
    {
        Vector3 aim = Input.mousePosition;
        aim = Camera.main.ScreenToWorldPoint(aim);
        Vector3 mouse = new Vector3(aim.x - transform.position.x, aim.y - transform.position.y, 0);
        Vector2 direction = new Vector2(aim.x - transform.position.x, aim.y - transform.position.y);
       
        
        
        if(count==0)
            {
            crosshair.SetActive(true);
            crshrKey = true;
            count=1;
           
        }

        if (mouse.magnitude > 0)
        {
            mouse.Normalize();
            mouse *= 1.12f;            
            crosshair.transform.localPosition = mouse;
            if (crshrKey == false)
            {
                crosshair.SetActive(true);
                crshrKey = true;
                
                
            }
            

            direction.Normalize();

            //Input keys for activating cards

            //HOKUS-POKE-US
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //useCard();
                GameObject attack = Instantiate(hocusPokeusPrefab, Vector3.Lerp(crosshair.transform.position, transform.position, 0.4f ), Quaternion.identity);
                //attack.GetComponent<Rigidbody2D>().velocity = direction;
                attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                Destroy(attack, 0.20f);
               
                //animator.SetBool("isBasic", true);
                //isMove = true;
            }

            //SLASH
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                //useCard();
                GameObject attack = Instantiate(slashPrefab, Vector3.Lerp(crosshair.transform.position, transform.position, 0.4f), Quaternion.identity);
                //attack.GetComponent<Rigidbody2D>().velocity = direction * 2.5f;
                attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                Destroy(attack, 0.30F);
                //isMove = true;
            }

            //DASH
            else if (Input.GetKeyDown(KeyCode.CapsLock))
            {
                //useCard();

                coll = GetComponent<Collider2D>();
                crosshair.SetActive(false);
                coll.isTrigger = true;
                GetComponent<Rigidbody2D>().velocity = direction * 9.7f;
                //transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                StartCoroutine(Dash(direction, coll));

            }

            //BEAM
            else if (Input.GetKeyDown(KeyCode.E))
            {
                //useCard();
                GameObject attack = Instantiate(beamPrefab, transform.position, Quaternion.identity);
                attack.GetComponent<Rigidbody2D>().velocity = direction * 4.20f;
                attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                
            }
        }
        else
        {
            crosshair.SetActive(false);
            //isMove = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="Enemy")
        {
            StartCoroutine("SpriteBlink");
            DecreaseHealth(2);
        }
      
        if (other.gameObject.tag == "Bottom Wall")
        {
            botVarY = transform.position.y;            

        }
        else if (other.gameObject.tag == "Top Wall")
        {
            topVarY = transform.position.y;
        }
        else if(other.gameObject.tag== "Left Wall") 
            {
            leftVarX = transform.position.x;
        }

        else if(other.gameObject.tag== "Right Wall")
            {
            rightVarX = transform.position.x;
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

    public void DecreaseHealth(float f)
    {
        curHealth -= f;
        float calcHealth = curHealth / maxHealth;
        SetHealthBar(calcHealth);
    }
    public void SetHealthBar(float f)
    {
        //healthBar.transform.localScale = new Vector3(f, 1, 1);
    }


}