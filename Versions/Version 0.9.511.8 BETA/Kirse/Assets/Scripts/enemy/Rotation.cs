using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public GameObject player;
    CardManager cm;
    int i;

    private void Start()
    {
        cm = CardManager.cm;
        player = GameObject.Find("Player");
        float AngleRad = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Owww");
            Destroy(gameObject);
            player.GetComponent<BasicMovment>().DecreaseHealth(15);

            if(this.tag=="bigbeam")
            {
                i = Random.Range(0, 4);
                if (cm.pHand[i].activeInHierarchy == true)
                {
                    Card c = cm.pHand[i].GetComponent<CardTemplate>().card;
                    CardProperties cp = cm.pHand[i].GetComponent<CardTemplate>().card.cardProperties;

                    cm.pHand[i].SetActive(false);
                    cm.discardPile.Add(cm.pHand[i].GetComponent<CardTemplate>().card);
                    //cm.usedCardType = c.cardType; // Sets the cardtype in BasicMovement to be accessed later
                    cm.pHand[i].GetComponent<CardTemplate>().card = null;
                }
            }
            

        }
    }
}
