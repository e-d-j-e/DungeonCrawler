using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    public GameObject startPanel;
    public GameObject escPanel;
    bool active = false;
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
    //public void TokenUpdate(int i)
    //{
    //    token += i;
    //    tokenText.text = token.ToString();
    //}

    //public void Awake()
    //{
    //    GameManager gm = this.gameObject.GetComponent<GameManager>();
    //}
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && startPanel.activeInHierarchy == false)
        {
            escPanel.SetActive(true);
            
        }
    }

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
    public void StartGame()
    {
        startPanel.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
