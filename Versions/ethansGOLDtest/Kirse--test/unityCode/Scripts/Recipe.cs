using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "EDJE/Recipe")]
public class Recipe : ScriptableObject
{
    public CardInfo card1;
    public CardInfo card2;
    public int reqToken;
    public CardInfo fusedCard;
}
