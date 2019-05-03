using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardManager : MonoBehaviour
{
    public CardType usedCardType;
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

    public GameObject cardResult;
    public Card empty;
    public float maxCards = 3;
    public float deckPercent;
    public GameObject forgePanel;
    public GameObject deckBar;
    public Text token;
    private int t = 0;
    public List<Card> forgeDeck = new List<Card>();

    BasicMovment player;
    public bool dub;

    public bool inMenu = false;
    public int i;
    public int lootCount;

    ForgeRoom fm;
    private static CardManager _instance;
    public static CardManager cm
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("GameManager").GetComponent<CardManager>();
            }

            return _instance;
        }
    }
    void Start()
    {
        recipeList = new List<Recipe>(Resources.LoadAll<Recipe>("Recipe"));
        lootCount = 0;
        token.text = t.ToString();
        dub = false;
        fm = GameObject.Find("Forge Room").GetComponent<ForgeRoom>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BasicMovment>();
        playerHand = GameObject.Find("PlayerHand");

        if (cardSelected == null) { }
        for (int i = 0; i < 4; i++)
        {
            pHand[i] = playerHand.transform.GetChild(i).gameObject;
            pHand[i].SetActive(false);
        }
        maxCards = playerDeck.Count;
        deckPercent = playerDeck.Count / maxCards;
        //for (int i = 0; i < recipeList.Count; i++)
        //{
        //    if (recipeList[i] is ReciipeTest)
        //    {
        //        Debug.Log("AWWWWWWWWWWWWWWWWWW");
        //    }
        //}

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

        if (Input.GetKeyDown(KeyCode.CapsLock)&& player.attacked==false)
        {
            UseCard(0);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && player.attacked == false)
        {
            UseCard(1);
        }
        else if (Input.GetKeyDown(KeyCode.E) && player.attacked == false)
        {
            UseCard(2);
        }
        else if (Input.GetKeyDown(KeyCode.R) && player.attacked == false)
        {
            UseCard(3);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            DrawCard(lootDeck);
            //DrawCard(lootDeck);

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
        if (pHand[i].activeInHierarchy == true)
        {
            Card c = pHand[i].GetComponent<CardTemplate>().card;
            CardProperties cp = pHand[i].GetComponent<CardTemplate>().card.cardProperties;

            pHand[i].SetActive(false);
            discardPile.Add(pHand[i].GetComponent<CardTemplate>().card);
            usedCardType = c.cardType; // Sets the cardtype in BasicMovement to be accessed later
            pHand[i].GetComponent<CardTemplate>().card = null;
            switch (cp.title)
            {
                case "Slash":
                    player.attacked = true;
                    player.Slash();
                    incToken();
                    
                    StartCoroutine(player.AttackRelease(0.420f));
                    break;
                case "Dash":
                    player.attacked = true;
                    player.Dash();
                    incToken();
                    StartCoroutine(player.AttackRelease(0.420f));
                    break;
                case "Beam":
                    player.attacked = true;
                    player.Beam();
                    incToken();
                    StartCoroutine(player.AttackRelease(0.420f));
                    break;
                case "Boomerang Dash":
                    player.attacked = true;
                    player.BoomerangDash();
                    incToken();
                    StartCoroutine(player.AttackRelease(0.420f));
                    break;
                case "SpinSlash":
                    player.attacked = true;
                    player.SpinSlash();
                    incToken();
                    StartCoroutine(player.AttackRelease(0.8f));
                    break;
                case "SuperBeam":
                    player.attacked = true;
                    player.SuperBeam();
                    incToken();
                    StartCoroutine(player.AttackRelease(0.8f));
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

                    //cardSelected = pHand[i].transform.gameObject;
                    pHand[i].GetComponent<CardTemplate>().LoadCard(c);
                    return;
                }
            }
        }
        //else { return; }
    }
    //public void CardTypeCheck()
    //{
    //    if (c.cardType is CardTypeSlash)
    //    {
    //        //c.gameObject
    //    }
    //}


    public void ForgeCard(Card card1, Card card2, Recipe recipe)
    {

        Recipe r = recipe;
        if (r == null) { Debug.Log("error1"); return; }

        if (r is SpecialRecipe)
        {
            if (card1 == r.card1 && t == r.reqToken || card2 == r.card1 && t == r.reqToken)
            {
                decToken(r.reqToken);
                cardResult.GetComponent<CardTemplate>().LoadCard(recipeList[i].fusedCard);

                for (int i = 0; i < 4; i++)
                {
                    if (pHand[i].activeInHierarchy == false)
                    {
                        //Debug.Log(pHand[i].activeInHierarchy);
                        //Debug.Log(pHand[i]);

                        pHand[i].SetActive(true);
                       
                        pHand[i].GetComponent<CardTemplate>().LoadCard(r.fusedCard);
                        //Debug.Log(pHand[i].activeInHierarchy);
                        //cardSelected = pHand[i].transform.gameObject;
                        //cardSelected.GetComponent<CardTemplate>().LoadCard(c);
                        fm.ResetForgeCards(card1, card2);
                        SetForgeDisplay();
                        return;
                    }
                   
                }
                discardPile.Add(r.fusedCard);
                fm.ResetForgeCards(card1, card2);
                SetForgeDisplay();
                //return;
            }
        }
            //    else
            //    {
            //        Debug.Log("error2");
            //        //discardPile.Add(r.fusedCard);
            //        //fm.ResetForgeCards(card1, card2);
            //        //SetForgeDisplay();
            //        //return;
            //    }
            //}
            //else if (r is Recipe)
            //{
            //    Debug.Log("Error 4");
            if (card1 == r.card1 && card2 == r.card2 && t >= r.reqToken || card1 == r.card2 && card2 == r.card1 && t >= r.reqToken)
            {
                decToken(r.reqToken);
                //cardResult.GetComponent<CardTemplate>().LoadCard(recipeList[i].fusedCard);
                for (int p = 0; p < 4; p++)
                {
                    if (pHand[p].activeInHierarchy == false)
                    {
                        pHand[p].SetActive(true);

                        cardSelected = pHand[p].transform.gameObject;
                        cardSelected.GetComponent<CardTemplate>().LoadCard(r.fusedCard);
                        fm.ResetForgeCards(card1, card2);
                        SetForgeDisplay();
                        return;
                    }

                }

            }
        //}
        else
        {
            Debug.Log("error3");
            discardPile.Add(r.fusedCard);
            fm.ResetForgeCards(card1, card2);
            SetForgeDisplay();
        }
    }
    public void SetForgeDisplay()
    {
        Color c = Color.white;
        c.a = 0;
        forge1Display.GetComponent<Image>().color = c; //After a fail, it will set transparency back to .3f
        forge2Display.GetComponent<Image>().color = c;
        cardResult.GetComponent<Image>().color = c;

    }


    public void incToken()
    {
        if (dub == true)
        {
            t += 2;
            token.text = t.ToString();
        }
        else if (t < 99)
        {
            t += 1;
            token.text = t.ToString();
        }

    }

    public void decToken(int d)
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

    public void CardToHand(Card c)
    {
        for (int i = 0; i < 4; i++)
        {
            if (pHand[i].activeInHierarchy == false)
            {
                //Debug.Log(pHand[i].activeInHierarchy);
                //Debug.Log(pHand[i]);

                pHand[i].SetActive(true);

                pHand[i].GetComponent<CardTemplate>().LoadCard(c);
                //Debug.Log(pHand[i].activeInHierarchy);
                //cardSelected = pHand[i].transform.gameObject;
                //cardSelected.GetComponent<CardTemplate>().LoadCard(c);
                return;

            }
        }

        discardPile.Add(c);


    }
    public void BonusEffects()
    {

    }
}

