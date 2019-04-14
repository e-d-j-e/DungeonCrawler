using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLoot : MonoBehaviour {

  
    private void OnTriggerEnter2D(Collider2D player)
    {
        Card loot;
        CardManager l = GameObject.Find("GameManager").GetComponent<CardManager>();
        int i = Random.Range(0, 3);
        loot = l.lootDeck[i];
        
        
        
        if (player.gameObject.tag == "Player")
        {
            l.discardPile.Add(loot);
            Destroy(gameObject);
        }
    }

}
