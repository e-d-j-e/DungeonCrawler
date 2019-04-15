using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLoot : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D player)
    {
        GameObject c = GameObject.FindGameObjectWithTag("GameController");
        CardManager cm = c.GetComponent<CardManager>();

        if (player.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            if (cm.playerDeck.Count != 0)
            {
                cm.DrawCard(cm.playerDeck);
                cm.deckPercent = cm.playerDeck.Count / cm.maxCards;
                Debug.Log(cm.deckPercent);
                cm.deckCalculate(cm.deckPercent);


            }
            else if (cm.playerDeck.Count == 0 && cm.discardPile.Count > 0)
            {
                for (int i = 0; i < cm.discardPile.Count; i++)
                {
                    cm.playerDeck.Add(cm.discardPile[i]);

                }

                cm.discardPile.Clear();
                cm.maxCards = cm.playerDeck.Count;
                cm.deckPercent = cm.playerDeck.Count / cm.maxCards;
                Debug.Log(cm.deckPercent);
                cm.deckCalculate(cm.deckPercent);
                cm.DrawCard(cm.playerDeck);
            }
            
        }

      
        
    }

}
