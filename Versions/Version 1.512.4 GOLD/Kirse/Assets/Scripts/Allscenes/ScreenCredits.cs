﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCredits : MonoBehaviour
{
    public GameObject credits;
    // Start is called before the first frame update
    void Start()
    {

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Time.timeScale = 0;
            credits.SetActive(true);
        }
    }
}
