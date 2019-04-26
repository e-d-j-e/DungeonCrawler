using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    public GameObject gameManager;
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
    //public int token = 0;
    public TextMeshProUGUI actionText;
    public TextMeshProUGUI forgeable;
   // public Text tokenText;

    private int slashCC = 0;
    //public void TokenUpdate(int i)
    //{
    //    token += i;
    //    tokenText.text = token.ToString();
    //}
   
    //public void Awake()
    //{
    //    GameManager gm = this.gameObject.GetComponent<GameManager>();
    //}

    
    public void SlowMo()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            for (float i = 1; i > .5f; i -= .1f)
            {
                Time.timeScale = i;
            }
        }
    }

    private void Start()
    {
        gameManager = this.gameObject;

    }
    public void LoadDetails(Card c)
    {
        
    }
    public void Update()
    {

    }
}
