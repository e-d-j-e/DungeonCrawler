using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HocusPokus : MonoBehaviour {
    public int damage = 20;
    CardManager cm;
    public void Start()
    {
        cm = GameObject.Find("GameManager").GetComponent<CardManager>();
        FindObjectOfType<AudioManager>().Play("HocusPokus");
    }

    private void OnTriggerEnter2D(Collider2D att)
    {
        enemyAI enemy = att.GetComponent<enemyAI>();
        if (enemy != null)
        {
            enemy.takeDamage(damage);
            if (cm.playerDeck.Count != 0)
            {
                int i = Random.Range(0, 2);
                if (i > 0)
                {
                    cm.DrawCard(cm.playerDeck);
                    cm.deckPercent = cm.playerDeck.Count / cm.maxCards;
                    Debug.Log(cm.deckPercent);
                    cm.deckCalculate(cm.deckPercent);
                }

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
