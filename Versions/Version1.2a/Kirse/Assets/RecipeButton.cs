using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeButton : MonoBehaviour {

    public Recipe recipe;
    public Card card1;
    public Card card2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadRecipe(Recipe r)
    {
        recipe = r;
        card1 = recipe.card1;
        card2 = recipe.card2;
    }
}
