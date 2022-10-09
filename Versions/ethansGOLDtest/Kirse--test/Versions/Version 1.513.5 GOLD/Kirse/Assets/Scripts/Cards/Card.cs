using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card")]
public class Card : ScriptableObject {

    public CardType cardType; // for creating an Object, with the string of the name
    public CardProperties cardProperties; // for creating the properties of : title(string), cardpic(Sprite), damage(int)

}
