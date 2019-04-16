using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLoot : MonoBehaviour {

  
    private void OnTriggerEnter2D(Collider2D player)
    {

        if (player.gameObject.tag == "Player")
        {
            CardManager cm = GameObject.Find("GameManager").GetComponent<CardManager>();
            Card loot;

            
            loot = cm.lootDeck[cm.loot()];


            cm.playerDeck.Add(loot);
            cm.DrawCard(cm.playerDeck);
            Destroy(gameObject);
        }

           
    }

    

}
