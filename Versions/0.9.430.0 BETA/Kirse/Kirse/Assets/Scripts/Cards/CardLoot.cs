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
        int ll=int.Parse(this.name);
          
        if (player.gameObject.tag == "Player")
        {
            if ((ll%2)==0)
            {
                ll--;
                GameObject gg= GameObject.Find(ll.ToString());
                cm.CardToHand(loot);
                Destroy(gg);
            }
            else
            {
                ll++;
                GameObject ggg=GameObject.Find(ll.ToString());
                cm.CardToHand(loot);
                Destroy(ggg);
            }
           
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
