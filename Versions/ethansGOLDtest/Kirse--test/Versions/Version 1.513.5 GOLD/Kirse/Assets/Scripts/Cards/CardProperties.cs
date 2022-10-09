using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CardProperties {

    public string title; // relateing to beam/dash/slash? // or is this one used for the object id check?
    public Sprite cardPic; // sprite of the card, the red/green/blue. 
    public int damage; // damage of the card when used. this should probably be the damage rolled for a hit attack. 
}
