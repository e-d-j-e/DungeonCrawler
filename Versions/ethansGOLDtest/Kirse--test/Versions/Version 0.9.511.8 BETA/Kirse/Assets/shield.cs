using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {       //we are able to use the tag "Attack" instead of beam  and use cardtypes to determine certain functions
        if (other.gameObject.tag == "beam" )
        {

            Destroy(other.gameObject);
            //INSERT HERE FOR BEAM REFLECTION IF WE WANT TO ADD
        }

    }
}
