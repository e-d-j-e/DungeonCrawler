using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ethan 
//March 28, 2019
//Version 0.20.00
//Desc: Basic Player movement script for the character player. Player has crosshair, WASD movement keys, 
//    Poke-Space Dash-Capslock Slash-Q Beam-E 
//    crosshair follows mouse around singular point, attacks and animations will follow this crosshair position
//
//    any tested animation stuff is commented out. 
//New on new version:
//
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
    //public Animator animator;

    public Vector3 camoffset;
    public Vector3 move;
    public Vector2 point;
    public Vector2 speed = new Vector2(2, 2);
    public Vector2 relativePosition;
    private Vector2 movement;
    private float movSpd = 01.5f;
    
     


    //SPRITE VARIABLES
    private string spriteNames = "dash";
    private int spriteVersion = 0;
    private SpriteRenderer spriteR;
    private Sprite[] sprites;
    Collider2D coll;

    private short count = 0;
    private bool crshrKey = false;

    // Start is called before the first frame update
    void Start()
    {    
           
            spriteR = GetComponent<SpriteRenderer>();
            sprites = Resources.LoadAll<Sprite>(spriteNames);
            camoffset = cam.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //movement for player
        move= new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0);
        transform.position+= ((movSpd*move)*Time.deltaTime);
        cam.transform.position = transform.position + camoffset;

        //gets mouse position and point and click 

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    //isMove = true;
        //}
        //transform.position = Vector2.MoveTowards(transform.position, point, (Time.deltaTime*1.5f));

        //relativePosition = new Vector2(point.x - transform.position.x, point.y - transform.position.y);


        //////animations for player
        //animator.SetFloat("Horizontal", move.x);
        //animator.SetFloat("Vertical", move.y);
        //animator.SetFloat("Magnitude", move.magnitude);


        aimCrosshair();

        /*while(isMove==true)
        {
            enemyMove();
            break;
        }*/


    }




    //movement restrictions, so player doesnt move so fast
    //void FixedUpdate()
    //{
    //    //x movement
    //    if(speed.x * Time.deltaTime >= Mathf.Abs(relativePosition.x))
    //    {
    //        movement.x = relativePosition.x;
    //       // isMove = true;
    //    }
    //    else
    //    {
    //        movement.x = speed.x * Mathf.Sign(relativePosition.x);
    //       // isMove = true;
    //    }
    //    //y movement
    //    if (speed.y * Time.deltaTime >= Mathf.Abs(relativePosition.y))
    //    {
    //        movement.y = relativePosition.y;
    //       // isMove = true;
    //    }
    //    else
    //    {
    //        movement.y = speed.y * Mathf.Sign(relativePosition.y);
    //        //isMove = true;
    //    }
    //    //transform.position = Vector2.MoveTowards(transform.position, movement, Time.deltaTime);
    //    GetComponent<Rigidbody2D>().velocity = movement*0.2f;
    //   // isMove = true;
    //}

    IEnumerator Example(Vector3 direction, Collider2D coll)
    {
        
        spriteR.sprite = sprites[spriteVersion];
        yield return new WaitForSeconds(.13f);
        
        GetComponent<Rigidbody2D>().velocity = direction * 0;
        spriteVersion = 1;
        spriteR.sprite = sprites[spriteVersion];
        transform.Rotate(0, 0, -(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
        spriteVersion = 0;
        coll.isTrigger = false;
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
            crosshair.SetActive(true);
            crshrKey = true;
            count=1;
           
        }

        if (mouse.magnitude > 0)
        {
            mouse.Normalize();
            mouse *= .4f;            
            crosshair.transform.localPosition = mouse;
            if (crshrKey == false)
            {
                crosshair.SetActive(true);
                crshrKey = true;
                
                
            }
            

            direction.Normalize();

            //Input keys for activating cards

            //BEAM
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //useCard();
                GameObject attack = Instantiate(hocusPokeusPrefab, transform.position, Quaternion.identity);
                attack.GetComponent<Rigidbody2D>().velocity = direction;
                attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                Destroy(attack, 0.26f);
                //isMove = true;
            }
            //SLASH
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                //useCard();
                GameObject attack = Instantiate(slashPrefab,transform.position, Quaternion.identity);
                attack.GetComponent<Rigidbody2D>().velocity = direction * 1.5f;
                attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                Destroy(attack, 0.32F);
                //isMove = true;
            }
            //DASH
            else if (Input.GetKeyDown(KeyCode.CapsLock))
            {
                //useCard();

                //MYOWN DASH EXPERIMENT:
                //insert sprite change, wait time while object moves towards mouse/crosshair position.change sprite back
                //spriteR.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);




                coll = GetComponent<Collider2D>();
                crosshair.SetActive(false);
                coll.isTrigger = true;
                
                GetComponent<Rigidbody2D>().velocity = direction * 8.8f;
                transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                StartCoroutine(Example(direction, coll));
                







                //END EXPERIMENT
                //GameObject attack = Instantiate(dashPrefab, transform.position, Quaternion.identity);
                //            attack.GetComponent<Rigidbody2D>().velocity = direction * 2.2f;
                //            attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                //           Destroy(attack, 2);
                ////isMove = true;
            }
            //ANY
            else if (Input.GetKeyDown(KeyCode.E))
            {
                //useCard();
                GameObject attack = Instantiate(beamPrefab, transform.position, Quaternion.identity);
                attack.GetComponent<Rigidbody2D>().velocity = direction * 3.5f;
                attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                Destroy(attack, 2);
                //isMove = true;
            }
        }
        else
        {
            crosshair.SetActive(false);
            //isMove = false;
        }
    }









}
