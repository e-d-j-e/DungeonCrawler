using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CardTemplate : MonoBehaviour {

    public string title;

    public TextMeshProUGUI titleText;
    public Image cardPicture;
    public CardInfo card;

    void Awake()
    {
        title = "1";
        titleText.text = title;
        gameObject.name = title;
    }

    public void CardUpdate(CardInfo c)
    {
        card = c;
        title = c.title;
        titleText.text = c.title;
        gameObject.name = c.title;
        transform.GetChild(0).GetComponent<Image>().sprite = c.cardPic;
    }
}
