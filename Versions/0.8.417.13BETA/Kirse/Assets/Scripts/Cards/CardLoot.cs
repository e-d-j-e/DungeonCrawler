using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLoot : MonoBehaviour
{
    Card loot;

    Sprite newSprite;
    CardManager cm;

    public void Start()
    {
        cm = CardManager.cm;
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        

        if (player.gameObject.tag == "Player")
        {
            

            cm.CardToHand(loot);
            Destroy(gameObject);
        }

           
    }
    public void LoadCard(Card c)
    {
        loot = c;
        newSprite = loot.cardProperties.cardPic;
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    

}
