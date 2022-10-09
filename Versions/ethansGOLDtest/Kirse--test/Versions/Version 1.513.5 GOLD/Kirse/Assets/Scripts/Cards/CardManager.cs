using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardManager : MonoBehaviour
{
    public CardType usedCardType;
    GameObject cardSelected;
    public GameObject playerHand;
    public List<Card> playerDeck = new List<Card>(); /* allocated memory for a list of objects of the Card type, so many cards with those attributes. 
    should this be the only list to use for the user hand, deck, forge, etc?
    or is it easier to make different lists, to split logic, rather then doing logic with the single list?   */
    public List<Card> lootDeck = new List<Card>();
    public List<Card> discardPile = new List<Card>(); // for the discard pile? whatever that is, not the hand? not the deck? after use?
    public List<GameObject> pHand = new List<GameObject>(); // play hands of cards, currently in the hand. able to use the cards, but also to forge?
    public List<Recipe> recipeList = new List<Recipe>(); // list for forge recipes, is this the list that is fucked up?
    /*TODO 0.1.9.10.2022
     * check if this is the list all the way down, and figure out what is stored in this. and how
     * then see what list is used, when calling/pressing the forge button, cuz if these objects exist, then maybe something else is wrong. 
     * forge might not work because we dont sem to call loadforgecard, and forge(), where do we click the button to forge?
     * 
     * */

    public int randomNum;
    public bool forgeable = false;
    // what are all these forges for? 
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
        recipeList = new List<Recipe>(Resources.LoadAll<Recipe>("Recipe")); // where are the resoucres??
        // what exactly is in the list? is it every card from the assets in unit inspector? 
         
        lootCount = 0; 
        token.text = t.ToString();
        dub = false;
        fm = GameObject.Find("Forge Room").GetComponent<ForgeRoom>(); // gets the game component, like the ui and shit i think, this should be fine?
        // check where the scrolling portion is. is it in here?
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


    public void ForgeCard(Card card1, Card card2)
    {

        for (short index = 0; index < recipeList.Count; index++)
        {

            // maybe try using the recipe list here instead of whatever recipe is?
            // Recipe r = recipe;

            if (recipeList[index] == null) { Debug.Log("Recipe ERROR - NULL"); return; } // our recipe is null!!!! 

            Debug.Log("not null OKAY!@");
            // first check if both cards are in the right place? is card 1 card 1 and card 2 card 2? AND vice versa
            if (card1 == recipeList[index].card1 && card2 == recipeList[index].card2 && t >= recipeList[index].reqToken || card1 == recipeList[index].card2 && card2 == recipeList[index].card1 && t >= recipeList[index].reqToken)
            {
                decToken(recipeList[index].reqToken);
                cardResult.GetComponent<CardTemplate>().LoadCard(recipeList[index].fusedCard);
                playerDeck.Add(recipeList[index].fusedCard);
                DrawCard(playerDeck);
                 // NOW FOLLOW THE RESULT OF THE CARD 
                 // playerDeck.Add(cardResult); // how to add this card now to the deck?
                 // what is the fused card? where do we set the fused card?
              //  CardToHand(recipeList[index].fusedCard); // does this add the corect forged card?
                
                Debug.Log("THIS IS THE CORRECT FORGING MESSAGE HELLO WOOO"); // 
                //for (int p = 0; p < 4; p++)
                //{
                //    if (pHand[p].activeInHierarchy == false)
                //    {
                //        pHand[p].SetActive(true);

                //        cardSelected = pHand[p].transform.gameObject;
                //        cardSelected.GetComponent<CardTemplate>().LoadCard(recipeList[index].fusedCard);
                //        fm.ResetForgeCards(card1, card2);
                //        SetForgeDisplay();
                //        return;
                //        Debug.Log("SOMETHIUNG ABOUT PLAYER HAAND");
                //    }

                //}

            }

            // checks for the first card slot, is either the first or second recipe card,
            if (card1 == recipeList[index].card1 && t == recipeList[index].reqToken || card2 == recipeList[index].card1 && t == recipeList[index].reqToken)
            {
                decToken(recipeList[index].reqToken);
                cardResult.GetComponent<CardTemplate>().LoadCard(recipeList[index].fusedCard); // gets the recipe list, does this list have the fused cards tho?
                Debug.Log("THIS IS THE OTHER MESSAGE HELLO WOOO");
                // this should check the playe hand, 4 hand slots. what are we checking here? if we click the card, it choose from the list, what is reset forge card?
                //for (int i = 0; i < 4; i++)
                //{
                //    if (pHand[i].activeInHierarchy == false)
                //    {
                //        //Debug.Log(pHand[i].activeInHierarchy);
                //        //Debug.Log(pHand[i]);

                //        pHand[i].SetActive(true);

                //        pHand[i].GetComponent<CardTemplate>().LoadCard(recipeList[index].fusedCard);
                //        //Debug.Log(pHand[i].activeInHierarchy);
                //        //cardSelected = pHand[i].transform.gameObject;
                //        //cardSelected.GetComponent<CardTemplate>().LoadCard(c);
                //        fm.ResetForgeCards(card1, card2); // this removes both the cards? huh?
                //        SetForgeDisplay();
                //        return;
                //    }

                //}
                discardPile.Add(recipeList[index].fusedCard);
                fm.ResetForgeCards(card1, card2); // why reset here?
                SetForgeDisplay();
                //return;
            }
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

