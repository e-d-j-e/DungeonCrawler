using UnityEngine.UI;
using UnityEngine;

public class tokenFont : MonoBehaviour {

    public Text tokens;
    private int t=0;
    // Update is called once per frame
    public void InctokenUI() {
        t++;
        tokens.text = t.ToString();

    }
    public void DectokenUI()
    {
        t--;
        tokens.text = t.ToString();

    }
}
