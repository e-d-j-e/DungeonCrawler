﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLoot : MonoBehaviour
{

    public GameObject BeamLootPrefab;
    bool open;
    CardManager cm;
    public GameObject circuitLootPrefab;
    // Start is called before the first frame update
    void Start()
    {
        cm = CardManager.cm;
        open = false;
    }

    //FOR NOW-MAKE CHEST DROP A MOD, ADD MOD EFFECT. 
    //CHEST ONLY OPENS ONCE!
    private void OnTriggerEnter2D(Collider2D ot)
    {
        //if(Input.GetKey(KeyCode.Space))
        //{
        if (ot.gameObject.tag == "Player" && open == false)
        {
            open = true;
            Vector2 lootdrop = new Vector2(transform.position.x+1.5f, transform.position.y - 2.5f);
            Instantiate(circuitLootPrefab, lootdrop, Quaternion.identity);
            //Vector2 lootdrop = new Vector2(transform.position.x - 3, transform.position.y - 3);
            //GameObject o = (GameObject)Instantiate(BeamLootPrefab, lootdrop, Quaternion.identity);
            //cm.lootCount++;
            //o.name = cm.lootCount.ToString();
            //Card loot;
            //loot = cm.lootDeck[cm.loot()];
            //o.GetComponent<CardLoot>().LoadCard(loot);

            //lootdrop = new Vector2(transform.position.x + 3, transform.position.y - 3);
            //GameObject o2 = (GameObject)Instantiate(BeamLootPrefab, lootdrop, Quaternion.identity);
            //cm.lootCount++;
            //o2.name = cm.lootCount.ToString();
            //Card loot2;
            //loot2 = cm.lootDeck[cm.loot()];
            //o2.GetComponent<CardLoot>().LoadCard(loot2);
        }
        //  }
    }
}