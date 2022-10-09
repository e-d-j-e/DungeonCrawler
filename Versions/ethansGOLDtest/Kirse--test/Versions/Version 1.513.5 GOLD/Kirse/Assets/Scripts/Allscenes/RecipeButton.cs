using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RecipeButton : MonoBehaviour {

    public Recipe recipe; // recipe object that has : card1(Card),card2(Card),reqToken(int),fusecard(Card_,cardName(string)
    public Card card1;
    public Card card2;
    public int token;
    public Card card3;
    CardManager cm;
    ForgeRoom fm;
    public Text recipeName;
    Color c;
    Color d;
    // Use this for initialization
    void Start ()
    {
        fm = GameObject.Find("Forge Room").GetComponent<ForgeRoom>();
        cm = GameObject.Find("GameManager").GetComponent<CardManager>();
        
        c = cm.forge1Display.GetComponent<Image>().color;
        d = c;
        d.a = 0;
        c.a = .3f;
        //LoadRecipe(); // why is this commented out? does this not work then? how do we get the recipes? is this called elsewherre?
    }


    // where do we load this recipe, and use the card results?
    public void LoadRecipe()
    {
        fm.recipe = recipe; // this sets the recipe forgeroom object, to the recipe. but what recipe??  this variable isnt defined anywhere here at least?

        cm.forge1Display.GetComponent<Image>().color = c;
        cm.forge2Display.GetComponent<Image>().color = c;
        cm.cardResult.GetComponent<Image>().color = c;

        if (recipe.card2 == null) { cm.forge2Display.GetComponent<Image>().color = d; }
        
        
        card1 = recipe.card1;
        card2 = recipe.card2;
        card3 = recipe.fusedCard;
        fm.UpdateToken(recipe.reqToken);
        cm.forge1Display.GetComponent<CardTemplate>().LoadCard(card1);
        cm.forge2Display.GetComponent<CardTemplate>().LoadCard(card2);
        cm.cardResult.GetComponent<CardTemplate>().LoadCard(card3); // we get the component here, for the gameobject, but now where is the actual forged card(Card)
    }
    public void SetRecipe(Recipe r)
    {
        recipe = r;
        string s = recipe.cardName;
        recipeName.text = s;
    }
}
