using UnityEngine;
using UnityEngine.UI;

public class tokens : MonoBehaviour
{
 
       
    public Text token;
    public short t = 0;

    void Start()
    {
        
        token.text = t.ToString();
       
    }

    //public void incToken()
    //{
    //    Debug.Log("we got to increment!");
    //    t++;
    //    token.text = t.ToString();

    //}

    //public void decToken(short d)
    //{
    //    t -= d;
    //    token.text = t.ToString();

    //}
}
