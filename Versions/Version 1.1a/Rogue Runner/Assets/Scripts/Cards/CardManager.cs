﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardManager : MonoBehaviour {
    GameObject cardSelected;
    public GameObject playerHand;
    public List<Card> playerDeck = new List<Card>();
    public List<Card> lootDeck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<GameObject> pHand = new List<GameObject>();
    public List<Recipe> recipeList = new List<Recipe>();
    public int randomNum;
    public bool forgeable = false;
    public GameObject forge1;
    public GameObject forge2;
    public GameObject forge1Display;
    public GameObject forge2Display;
    GameManager gm;
    public GameObject cardTest;
    public Card empty;
    public float maxCards = 3;
    public float deckPercent;
    public GameObject forgePanel;
    public GameObject deckBar;

    BasicMovment player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BasicMovment>();
        gm = GameManager.gm;
        if (cardSelected == null) { }
        for (int i = 0; i < 4; i++)
        {
            pHand[i] =playerHand.transform.GetChild(i).gameObject;
            pHand[i].SetActive(false);
        }
        
    }
    void Update()
    {
        Controls();
    }


    public void Controls()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Forging();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseCard(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseCard(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseCard(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseCard(3);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            DrawCard(lootDeck);
            //DrawCard(lootDeck);
            //gm.token++;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DrawCard(discardPile);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerDeck.Count != 0)
            {
                DrawCard(playerDeck);
                deckPercent = playerDeck.Count / maxCards;
                Debug.Log(deckPercent);
                deckCalculate(deckPercent);


            }
            else if (playerDeck.Count == 0 && discardPile.Count > 0)
            {
                for (int i = 0; i < discardPile.Count; i++)
                {
                    playerDeck.Add(discardPile[i]);

                }

                discardPile.Clear();
                maxCards = playerDeck.Count;
                deckPercent = playerDeck.Count / maxCards;
                Debug.Log(deckPercent);
                deckCalculate(deckPercent);
                DrawCard(playerDeck);
            }
        }
    }
    public void deckCalculate(float f)
    {

        deckBar.transform.localScale = new Vector3(f, deckBar.transform.localScale.y, deckBar.transform.localScale.z);

        //else { deckBar.transform.localScale = new Vector3(1, deckBar.transform.localScale.y, deckBar.transform.localScale.z); }
    }
 

    public void UseCard(int i)
    {
        GameObject o = pHand[i];

        if (o.activeInHierarchy == true && forgeable == false)
        {

            Card c = o.transform.GetChild(0).gameObject.GetComponent<CardTemplate>().card;
            CardProperties cp = c.cardProperties;
            pHand[i].SetActive(false);
            discardPile.Add(c);
            switch (cp.title)
            {
                case "Slash":
                    gm.actionText.text = cp.title;
                    //enter dmg calculator
                    break;
                case "Dash":
                    gm.actionText.text = cp.title;
                    break;
                case "Beam":
                    gm.actionText.text = cp.title;
                    player.Beam();
                    break;
                case "DashBeam":
                    gm.actionText.text = cp.title;
                    break;
                default:
                    break;
            }
        }
        if (o.activeInHierarchy == true && forgeable == true)
        {
            if (forge1 == null)
            {
                forge1 = o;
                Card c = o.transform.GetChild(0).gameObject.GetComponent<CardTemplate>().card;
                forge1Display.GetComponent<CardTemplate>().LoadCard(c);
                return;
            }
            else if (forge1 != null)
            {
                forge2 = o;
                Card c = o.transform.GetChild(0).gameObject.GetComponent<CardTemplate>().card;
                forge2Display.GetComponent<CardTemplate>().LoadCard(c);
                ForgeCard(forge1, forge2);
            }
        }
    }

    public void DrawCard(List<Card> list)
    {
        Debug.Log(list.Count);
        if (list.Count > 0)
        {
            for (int i = 0; i < 4; i++)
            {
                if (pHand[i].activeInHierarchy == false)
                {

                    randomNum = Random.Range(0, list.Count);
                    Card c = list[randomNum];
                    //CardTypeCheck(c);
                    if (list != lootDeck)
                    {
                        int d = list.Count;
                        Debug.Log("List Count: " + d);
                        list.RemoveAt(randomNum);
                    }
                    pHand[i].SetActive(true);

                    cardSelected = pHand[i].transform.GetChild(0).gameObject;
                    cardSelected.GetComponent<CardTemplate>().LoadCard(c);
                    if (list == lootDeck)
                    {
                        gm.TokenUpdate(1);
                        gm.actionText.text = "Draw from Loot Deck";
                    }
                    if (list == playerDeck)
                    {

                        gm.actionText.text = "Draw from Player Deck";
                    }
                    if (list == discardPile)
                    {

                        gm.actionText.text = "Draw from Discard Deck";
                    }
                    return;
                }
            }
        }
        else { return; }
    }
    //public void CardTypeCheck()
    //{
    //    if (c.cardType is CardTypeSlash)
    //    {
    //        //c.gameObject
    //    }
    //}
    public void ForgeCard(GameObject a, GameObject b)
    {
        Card c = a.transform.GetChild(0).gameObject.GetComponent<CardTemplate>().card;
        Card d = b.transform.GetChild(0).gameObject.GetComponent<CardTemplate>().card;

        for (int i = 0; i < recipeList.Count; i++)
        {
            Recipe r = recipeList[i];
            if (c == r.card1 && d == r.card2 && gm.token >= r.reqToken
                || c == r.card2 && d == r.card1 && gm.token >= r.reqToken)
            {
                gm.actionText.text = recipeList[i].name + " Forging Complete";
                gm.TokenUpdate(-r.reqToken);
                a.transform.GetChild(0).gameObject.GetComponent<CardTemplate>().LoadCard(recipeList[i].fusedCard);
                cardTest.GetComponent<CardTemplate>().LoadCard(recipeList[i].fusedCard);
                b.SetActive(false);
                Forging();
                return;
            }
            else
            {
                forge1 = null;
                forge2 = null;
                forge1Display.GetComponent<CardTemplate>().LoadCard(empty);
                forge2Display.GetComponent<CardTemplate>().LoadCard(empty);
                cardTest.GetComponent<CardTemplate>().LoadCard(empty);
                gm.forgeable.text = "try again";//"Forgeable : " + forgeable;
            }
        }
        Forging();
        return;
    }
    public void Forging()
    {
        if (forgePanel.activeInHierarchy == true)
        {
            forgeable = true;
            //forgeable = !forgeable;
            if (forgeable == true)
                gm.forgeable.text = "Forgeable : " + forgeable;
            if (forgeable == false)
            {
                forge1 = null;
                forge2 = null;
                forge1Display.GetComponent<CardTemplate>().LoadCard(empty);
                forge2Display.GetComponent<CardTemplate>().LoadCard(empty);
                gm.forgeable.text = "try again";//"Forgeable : " + forgeable;
            }
        }
        else { forgeable = false; }
    }

}