using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForgeRoom : MonoBehaviour
{

    public GameObject forgePanel;
    bool activePanel = false;
    public CardManager cm; // bro how are we using cardmanager and forge room within eachother? were use on in each file, wtf? are they just references, but if so why?
    public GameObject cardButton;
    public Transform cardButtonParent;
    public GameObject recipeButton;
    public Transform recipeButtonParent;
    public List<Card> forgeDeck = new List<Card>(); // now is this the deck that is used to forge

    public Card forge1;
    public Card forge2;
    public Text tokenText; // what text does this relate to? the ui on the forge screen? the scroll, or the text hovering the card on select?
    int token;

    public Recipe recipe; // another recipe? 
    // Use this for initialization
    void Start()
    {
        cm = GameObject.Find("GameManager").GetComponent<CardManager>();
        //forgePanel = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SetForgePanel(activePanel); // and we call this in the forge room, we are actiively setting the forge panel. 
            //cm.Forging();
            //Time.timeScale = 1;
            //for (int i = 0; i < forgeDeck.Count; i++)
            //{
            //    cm.playerDeck.Add(forgeDeck[i]);

            //}
            //foreach (Transform child in recipeButtonParent)
            //{
            //    GameObject.Destroy(child.gameObject);
            //}
            //foreach (Transform child in cardButtonParent)
            //{
            //    GameObject.Destroy(child.gameObject);
            //}
            //forgeDeck.Clear();
        }
    }

    void SetForgePanel(bool b)
    {
        activePanel = !b;
        forgePanel.SetActive(activePanel);
        //cm.Forging();
        Time.timeScale = 0;
        CreateForgeDeck();
        RecipeButtonLoad(); // makes gameobjects containing all the recipes? the fgameobjects get the component of recipebutton.
        if (activePanel == false)
        {
            Time.timeScale = 1;
            for (int i = 0; i < forgeDeck.Count; i++)
            {
                cm.playerDeck.Add(forgeDeck[i]); //we create forgedeck by getting the playerdeck, then set the playerdeck back to the forgedeck? 
               // isActiveAndEnabled this after the forge is done??

            }
            foreach (Transform child in recipeButtonParent)
            {
                GameObject.Destroy(child.gameObject);
            }
            foreach (Transform child in cardButtonParent)
            {
                GameObject.Destroy(child.gameObject);
            }
            forgeDeck.Clear();
            cm.SetForgeDisplay();
            tokenText.text = "";
            forge1 = null;
            forge2 = null;
        }

    }
    public void RecipeButtonLoad()
    {
        for (int i = 0; i < cm.recipeList.Count; i++)
        {
            GameObject o = Instantiate(recipeButton, recipeButton.transform.parent); // what are we doing here? instantiateing with a recipe? 
            o.transform.SetParent(recipeButtonParent, false);
            o.GetComponent<RecipeButton>().SetRecipe(cm.recipeList[i]); // then getting the recipe button, from the cm.recipelist. 
            // we alreaduy have the recipe list, what game object utilizes the recipe list, other than forging?
        }
    }
    public void CreateForgeDeck() // this deck should have all the cards for the forging.
    {

        for (int i = 0; i < cm.playerDeck.Count; i++)
        {
            if (cm.playerDeck[i] != null) // this doesnt seem to work, it doesnt show cards from the players hands. 
                forgeDeck.Add(cm.playerDeck[i]);
        }
        //if (cm.pHand.Count > 0)
        //{
        //    for (int i = 0; i < cm.pHand.Count; i++)
        //    {
        //        Card c = cm.pHand[i].transform.GetChild(0).GetComponent<CardTemplate>().card;
        //        if (c != null)
        //            forgeDeck.Add(c);

        //    }
        //}
        if (cm.discardPile.Count > 0)
        {
            for (int i = 0; i < cm.discardPile.Count; i++)
            {
                if (cm.discardPile[i] != null)
                    forgeDeck.Add(cm.discardPile[i]);
            }
        }
        cm.discardPile.Clear();
        cm.playerDeck.Clear();
        CreateCardButton();

    }
    public void Forge()
    {
        
        cm.ForgeCard(forge1, forge2); // so here we use recipe, should this be the recipes from the cardmanager?
        Debug.Log("Forging"); // so this does get called, but where??
    }
    public void CreateCardButton()
    {
        for (int i = 0; i < forgeDeck.Count; i++)
        {
            GameObject o = Instantiate(cardButton, cardButton.transform.parent);
            //o.transform.parent = cardButtonParent.transform;
            o.transform.SetParent(cardButtonParent, false);
            o.GetComponent<CardTemplate>().LoadCard(forgeDeck[i]);
        }
    }
    public void UpdateToken(int i)
    {
        token = i;
        tokenText.text = token.ToString();
    }
    public void ResetForgeCards(Card card1, Card card2)
    {
        forgeDeck.Remove(card1);
        forgeDeck.Remove(card2);
        forge1 = null;
        forge2 = null;
        //add fused card to 
        foreach (Transform child in cardButtonParent)
        {
            GameObject.Destroy(child.gameObject);
        }
        CreateCardButton();

    }
}
