using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CardTemplate : MonoBehaviour {
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI dmgText;
    public Image cardPicture;
    public Card card;
    public Card loadingCard;

    public void LoadCard(Card c)
    {
        if (c == null)
            return;
        card = c;
        CardProperties cp = c.cardProperties;
        gameObject.name = cp.title;
        titleText.text = c.cardProperties.title;
        dmgText.text = cp.damage.ToString();
        cardPicture.sprite = c.cardProperties.cardPic;
        if(c.cardType is CardTypeSlash)
        {
            GameManager.gm.slashCardCountUpdate(1);
        }
    }
}
