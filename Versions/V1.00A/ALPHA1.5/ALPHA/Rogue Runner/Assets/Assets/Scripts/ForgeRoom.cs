using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeRoom : MonoBehaviour {

    public GameObject forgePanel;
    bool b = false;
    CardManager cm;
	// Use this for initialization
	void Start () {
        cm = GameObject.Find("GameManager").GetComponent<CardManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) { forgePanel.SetActive(b); Time.timeScale = 1; }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {

            //b = !b;

            forgePanel.SetActive(!b);
            cm.Forging();
            Time.timeScale = 0;
            
        }
        
    }
   
}
