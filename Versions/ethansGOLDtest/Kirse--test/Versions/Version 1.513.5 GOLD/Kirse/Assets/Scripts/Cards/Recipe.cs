using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "EDJE/Recipe")]
public class Recipe : ScriptableObject
{
    public Card card1; // card number one, contains all that juicy card info, should prob be used to object id checking? 
    //probably deleted after they forge together? 
    public Card card2; // card 2 ^
    public int reqToken; // amount of tokens required to forge the cards, should be displayed, each forgoe recipe has a differenta amount/
    public Card fusedCard; // thew new card, presumably to be added back to the list of cards. also why fuse? y not forge lmao..
    public string cardName;// what is this for? if the fused card is a card type it should have a name for id there, 
}
