﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager gm
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("GameManager").GetComponent<GameManager>();
            }

            return _instance;
        }
    }
    public int token = 0;
    public TextMeshProUGUI actionText;
    public TextMeshProUGUI forgeable;
    public TextMeshProUGUI tokenText;

    public void TokenUpdate(int i)
    {
        token += i;
        tokenText.text = "Tokens : " + token;
    }
    //public void Awake()
    //{
    //    GameManager gm = this.gameObject.GetComponent<GameManager>();
    //}
}
