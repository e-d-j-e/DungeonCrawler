﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CardTemplate : MonoBehaviour {
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI dmgText;
    public Image cardPicture;
    public Card card;
    public Sprite slash;
    public Sprite dash;
    public Sprite beam;

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
        if (c.cardType is CardTypeSlash)
        {
            gameObject.GetComponent<Image>().sprite = slash; 
            //GameManager.gm.slashCardCountUpdate(1);
        }
        if (c.cardType is CardTypeBeam)
        {
            gameObject.GetComponent<Image>().sprite = beam;
            //GameManager.gm.slashCardCountUpdate(1);
        }
        if (c.cardType is CardTypeDash)
        {
            gameObject.GetComponent<Image>().sprite = dash;
            //GameManager.gm.slashCardCountUpdate(1);
        }
        if (c.cardType == null)
        { gameObject.GetComponent<Image>().sprite = null; }
    }
}
