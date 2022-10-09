using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject toolTip;
    public Text toolText;
    public GameObject startscreen;
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
    public TextMeshProUGUI actionText;
    public TextMeshProUGUI forgeable;  
    private int slashCC = 0;
    public GameObject pauseMenu;
    bool paused;
    bool playmusic;
    
   
    private void Awake()
    {
        Time.timeScale = 0;
        gameManager = this.gameObject;
        toolTip.SetActive(false);
        paused = false;
        startscreen.SetActive(true);
        playmusic = false;
        

    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Time.timeScale = 1;
            startscreen.SetActive(false);
            
        }
        if (playmusic == false)
        {
            FindObjectOfType<AudioManager>().Play("music");
            playmusic = true;

        }
        
    }
    public void LoadDetails(string s,bool b)
    {
        toolTip.SetActive(b);
        toolText.text = s;
    }
  
    public void pauseGame()
    {
        //Unpause
        if (paused)
        {
            Time.timeScale = 1;
            paused = false;
            pauseMenu.SetActive(false);
            //FindObjectOfType<AudioManager>().Play("unpause");
        }
        //Pause
        else
        {
            Time.timeScale = 0;
            paused = true;
            pauseMenu.SetActive(true);
            //FindObjectOfType<AudioManager>().Play("pause");

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
}
