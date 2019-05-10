using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "EDJE/Recipe")]
public class Recipe : ScriptableObject
{
    public Card card1;
    public Card card2;
    public int reqToken;
    public Card fusedCard;
    public string cardName;
}
