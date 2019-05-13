using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pits3 : MonoBehaviour
{
    //maybe change gravity on player so it looks like they are falling
    public bool point3 = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            point3 = true;
        }
    }
}
