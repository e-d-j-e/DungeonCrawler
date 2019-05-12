using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pits : MonoBehaviour
{
    public GameObject player;
    Vector3 ppos;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject==player)
        {
            ppos = player.transform.position;

        }
    }
}
