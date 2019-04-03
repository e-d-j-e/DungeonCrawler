using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public Animator animator;
    public Vector2 targetPosition;
    public float charMoveX;
    public float charMoveY;
    public Vector3 charMovement;
    public Vector2 charMovement2;
    public Vector3 charPosition;
    public Vector3 enemyMovement;
    public float enemyX;
    public float enemyY;
    public float speed = 2;
    public GameObject enemy;
    float highVarY = 10;
    float lowVarY = -5;
    float leftVarX = -5;
    float rightVarX = -5;
    public Vector3 lastKey;
    bool facingRight;
    private void Start()
    {
        //transform.position
        targetPosition = transform.position;
        charMovement = new Vector3(3, 0, 0);

    }
    void FixedUpdate () {

        //targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = targetPosition * Time.deltaTime;
        //float step = 5 * Time.deltaTime; // calculate distance to move
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        //Controls();
        //if (transform.position != charPosition)
        //{
        //    if (enemy == null) { return; }
        //    else
        //    {
        //        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, transform.position, 1 * Time.deltaTime);
        //    }
        //}

        //if (transform.position.y >= highVarY) { transform.position = new Vector3(transform.position.x, highVarY, 0); }
        ////if (transform.position.y <= lowVarY) { transform.position = new Vector3(transform.position.x, lowVarY, 0); }
        ////if (transform.position.y <= lowVarY) { transform.position = new Vector3(transform.position.x, lowVarY, 0); }
        //if (transform.position.y <= lowVarY) { transform.position = new Vector3(transform.position.x, lowVarY, 0); }

        var move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        animator.SetFloat("Horizontal", move.x);
        animator.SetFloat("Vertical", move.y);
        animator.SetFloat("Magnitude", move.magnitude);

        transform.position += move * speed * Time.deltaTime;
        Controls();
        Flip(Input.GetAxisRaw("Horizontal"));
    }
    void Controls()
    {
        //if (Input.GetKey(KeyCode.RightArrow))
        //transform.position += Vector3.right * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
 
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            charPosition = new Vector3(targetPosition.x, targetPosition.y, 0);

            //enemyMovement = new Vector3
            //charMoveX = gameObject.transform.position.x + targetPosition.x;
            //charMoveY = gameObject.transform.position.y + targetPosition.y;
            //charMovement = new Vector3(charMoveX, charMoveY,0);

            //transform.position += Vector3.Lerp(transform.position, charMovement, 1*Time.deltaTime);
        
        }
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    lastKey = Vector3.left;
        //    transform.Translate(Vector3.left * 3);
        //    //transform.position += Input.GetAxis("Horizontal")
        //}
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            lastKey = Vector3.right;
            //animator.Play("Kirse_runRight");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            lastKey = Vector3.up;
            //animator.Play("Kirse_runUp");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            lastKey = Vector3.down;
            //animator.Play("Kirse_runDown");
        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Bottom Wall")
        {
            lowVarY = transform.position.y;
            Debug.Log(transform.position);
           
        }
        if (other.gameObject.name == "Top Wall")
        {
            highVarY = transform.position.y;
        }
    }
    void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
