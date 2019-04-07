using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public GameObject player;
    public Vector3 atkPos;
    public float speed;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        atkPos = player.transform.position - transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        
        transform.position += atkPos* speed*Time.deltaTime;
        
       
        //transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 10);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            player.GetComponent<PlayerMove>().DecreaseHealth(2);
            Debug.Log("Oww"); 
}
        Destroy(gameObject);
    }
    void Calculate()
    {
        atkPos = player.transform.position - transform.position;
    }
}
