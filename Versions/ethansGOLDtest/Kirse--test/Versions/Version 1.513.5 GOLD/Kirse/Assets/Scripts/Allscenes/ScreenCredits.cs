using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCredits : MonoBehaviour
{
    public GameObject credits;
    public GameObject point;
    static int  l = 0;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            l++;
            if (l==1)
            {
                player.transform.position = point.transform.position;
            }
            else if (l>=2)
            {
                Time.timeScale = 0;
                credits.SetActive(true);
            }
            
        }
    }
}
