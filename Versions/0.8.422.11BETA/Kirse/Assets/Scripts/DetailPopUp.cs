using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DetailPopUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float startDelay = 2;
    public float currentTime;
    public GameObject panel;
    Text text;
    private void Start()
    {

        panel = Resources.Load<GameObject>("UI");
        //
        //text = panel.GetComponentInChildren<Text>();
        //panel.SetActive(false);
    }
    private void Update()
    {
        if (panel == null) { return; }
        if (panel.activeInHierarchy == true)
        {
            panel.transform.position = Input.mousePosition;
        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("The cursor entered the selectable UI element.");
        currentTime -= Time.deltaTime;


        if (currentTime <= 0)
        {
            if (panel == null) { return; }
            panel.SetActive(true);
            text.text = "Asdfasdf \n asdfasdf";


        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }
    void OnMouseOver()
    {

        currentTime -= Time.deltaTime;
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
        if (currentTime <= 0)
        {
            if (panel == null) { return; }
            panel.SetActive(true);
            text.text = "Asdfasdf \n asdfasdf";


        }



    }

    void OnMouseExit()
    {
        startDelay = 2;


        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
        if (panel != null) { panel.SetActive(false); }
    }
}
