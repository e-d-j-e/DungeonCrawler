using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLoot : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D player)
    {
        Card loot = new Card();
        CardManager l = new CardManager();

        if (player.gameObject.tag == "Player")
        {
            //add card to hand
            //l.DrawCard(loot);        

            Destroy(gameObject);
        }
    }

}
