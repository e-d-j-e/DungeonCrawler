using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
    CardManager cm;
    ForgeRoom fm;
    Color c;
    public void Start()
    {
        fm = GameObject.Find("Forge Room").GetComponent<ForgeRoom>();
        cm = GameObject.Find("GameManager").GetComponent<CardManager>();
        c = cm.forge1Display.GetComponent<Image>().color;
        c.a = 1;
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            fm.forge1 = gameObject.GetComponent<CardTemplate>().card;
            cm.forge1Display.GetComponent<Image>().color = c;
            cm.forge1Display.GetComponent<CardTemplate>().LoadCard(gameObject.GetComponent<CardTemplate>().card);
            Debug.Log("Left click");
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right click");
            fm.forge2 = gameObject.GetComponent<CardTemplate>().card;
            cm.forge2Display.GetComponent<CardTemplate>().LoadCard(gameObject.GetComponent<CardTemplate>().card);
            cm.forge2Display.GetComponent<Image>().color = c;
        }
    }
}