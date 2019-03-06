using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO:
/*4 key inputs
 *hocus poke-us attack input
 * 
 * 
 */


public class BasicMovment : MonoBehaviour
{
   //yes lol 
    public bool isMove;
    public GameObject hocusPokeusPrefab;
    public GameObject slashPrefab;
    public GameObject dashPrefab;
    public GameObject beamPrefab;

    public GameObject crosshair;
    public Vector2 point;


    public Vector2 speed = new Vector2(2, 2);
    public Vector2 relativePosition;
    private Vector2 movement;

   


    //public Animator animator; 


    // Start is called before the first frame update
    void Start()
    {
        
             
    }

    // Update is called once per frame
    void Update()
    {
        //movement for player
        //Vector3 move= new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0);
        //transform.position+= ((0.7f*move)*Time.deltaTime);
        
        //gets mouse position and point and click 
       
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //isMove = true;
        }
        //transform.position = Vector2.MoveTowards(transform.position, point, (Time.deltaTime*1.5f));

        relativePosition = new Vector2(point.x - transform.position.x, point.y - transform.position.y);


        //animations for player
        //animator.SetFloat("Horizontal", move.x);
        //animator.SetFloat("Vertical", move.y);
        // animator.SetFloat("Magnitdue", move.magnitude);


        aimCrosshair();

        /*while(isMove==true)
        {
            enemyMove();
            break;
        }*/


    }

    //movement restrictions, so player doesnt move so fast
   void FixedUpdate()
    {
        //x movement
        if(speed.x * Time.deltaTime >= Mathf.Abs(relativePosition.x))
        {
            movement.x = relativePosition.x;
           // isMove = true;
        }
        else
        {
            movement.x = speed.x * Mathf.Sign(relativePosition.x);
           // isMove = true;
        }
        //y movement
        if (speed.y * Time.deltaTime >= Mathf.Abs(relativePosition.y))
        {
            movement.y = relativePosition.y;
           // isMove = true;
        }
        else
        {
            movement.y = speed.y * Mathf.Sign(relativePosition.y);
            //isMove = true;
        }
        //transform.position = Vector2.MoveTowards(transform.position, movement, Time.deltaTime);
        GetComponent<Rigidbody2D>().velocity = movement*0.2f;
       // isMove = true;
    }



    //Crosshair follows mouse movements relative to players position
    private void aimCrosshair()
    {
        Vector3 aim = Input.mousePosition;
        aim = Camera.main.ScreenToWorldPoint(aim);
        Vector2 mouse = new Vector2(aim.x-transform.position.x, aim.y-transform.position.y);
        Vector2 direction = new Vector2(aim.x-transform.position.x, aim.y-transform.position.y);
               

        if (mouse.magnitude>0)
        {
            mouse.Normalize();
            mouse *= .4f;
            crosshair.transform.localPosition = mouse;
            crosshair.SetActive(true);

            direction.Normalize();
            
            //Input keys for activating cards
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //useCard();
                GameObject attack = Instantiate(hocusPokeusPrefab, transform.position, Quaternion.identity);
                attack.GetComponent<Rigidbody2D>().velocity = direction;
                attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                Destroy(attack, .26f);
                //isMove = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //useCard();
                GameObject attack = Instantiate(slashPrefab, transform.position, Quaternion.identity);
                attack.GetComponent<Rigidbody2D>().velocity = direction * 1.5f;
                attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                Destroy(attack, 2);
                //isMove = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                //useCard();
                GameObject attack = Instantiate(dashPrefab, transform.position, Quaternion.identity);
                attack.GetComponent<Rigidbody2D>().velocity = direction * 2.2f;
                attack.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                Destroy(attack, 2);
                //isMove = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
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
