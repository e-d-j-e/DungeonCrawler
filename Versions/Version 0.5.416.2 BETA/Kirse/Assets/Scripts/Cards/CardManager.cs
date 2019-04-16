using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardManager : MonoBehaviour
{
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
    public GameObject cardResult;
    public Card empty;
    public float maxCards = 3;
    public float deckPercent;
    public GameObject forgePanel;
    public GameObject deckBar;
    public Text token;
    private short t=0;
    public List<Card> forgeDeck = new List<Card>();

    BasicMovment player;

    public bool inMenu = false;
    public int i;

    ForgeRoom fm;

    void Start()
    {
        token.text = t.ToString();
        fm = GameObject.Find("Forge Room").GetComponent<ForgeRoom>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BasicMovment>();
        playerHand = GameObject.Find("PlayerHand");
        gm = GameManager.gm;
        if (cardSelected == null) { }
        for (int i = 0; i < 4; i++)
        {
            pHand[i] = playerHand.transform.GetChild(i).gameObject;
            pHand[i].SetActive(false);
        }

    }
    public int loot()
    {
        i = Random.Range(0, 3);
        return i;
    }
    void Update()
    {
        if (inMenu == false)
            Controls();


    }


    public void Controls()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Forging();
        }
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            UseCard(0);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseCard(1);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UseCard(2);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            UseCard(3);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            DrawCard(lootDeck);
            //DrawCard(lootDeck);
            //gm.token++;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            DrawCard(discardPile);
        }
        if (Input.GetKeyDown(KeyCode.L))
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
            //gm.TokenUpdate(1);
            Card c = o.transform.GetChild(0).gameObject.GetComponent<CardTemplate>().card;
            CardProperties cp = c.cardProperties;

            pHand[i].SetActive(false);
            discardPile.Add(c);
            o.transform.GetChild(0).gameObject.GetComponent<CardTemplate>().card = null;
            switch (cp.title)
            {
                case "Slash":
                   // gm.actionText.text = cp.title;
                    player.Slash();
                    incToken();
                    break;
                case "Dash":
                   // gm.actionText.text = cp.title;
                    player.Dash();
                    incToken();
                    break;
                case "Beam":
                    //gm.actionText.text = cp.title;
                    player.Beam();
                    incToken();
                    break;
                case "DashBeam":
                   // gm.actionText.text = cp.title;

                    break;
                default:
                    break;
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

                        //gm.actionText.text = "Draw from Loot Deck";
                    }
                    if (list == playerDeck)
                    {

                        //gm.actionText.text = "Draw from Player Deck";
                    }
                    if (list == discardPile)
                    {

                       // gm.actionText.text = "Draw from Discard Deck";
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


    public void ForgeCard(Card card1, Card card2)
    {
        for (int i = 0; i < recipeList.Count; i++)
        {
            Recipe r = recipeList[i];
            if (card1 == r.card1 && card2 == r.card2 && t >= r.reqToken
                || card1 == r.card2 && card2 == r.card1 && t >= r.reqToken)
            {
                // gm.actionText.text = recipeList[i].name + " Forging Complete";
                // gm.TokenUpdate(-r.reqToken);
                //a.transform.GetChild(0).gameObject.GetComponent<CardTemplate>().LoadCard(recipeList[i].fusedCard);
                cardResult.GetComponent<CardTemplate>().LoadCard(recipeList[i].fusedCard);
                //b.SetActive(false);
                //Forging();
                fm.forgeDeck.Add(recipeList[i].fusedCard);
                fm.ResetForgeCards(card1, card2);
                Debug.Log("Yay");
                return;
            }
            else
            {
                forge1 = null;
                forge2 = null;
                forge1Display.GetComponent<CardTemplate>().LoadCard(empty);
                forge2Display.GetComponent<CardTemplate>().LoadCard(empty);
                cardResult.GetComponent<CardTemplate>().LoadCard(empty);
                Debug.Log("aww");
                gm.forgeable.text = "try again";//"Forgeable : " + forgeable;
            }
        }
        //Forging();
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


    public void incToken()
    {
        if (t < 99)
        {
            t += 1;
            token.text = t.ToString();
        }

    }

    public void decToken(short d)
    {
        if (testDec(t, d) == true)
        {
            t -= d;
            token.text = t.ToString();
        }
    }

    private bool testDec(int t, int d)
    {
        //return true if can decrement the amount of tokens used.
        return ((t -= d) >= 0 ? true : false);
    }

}
