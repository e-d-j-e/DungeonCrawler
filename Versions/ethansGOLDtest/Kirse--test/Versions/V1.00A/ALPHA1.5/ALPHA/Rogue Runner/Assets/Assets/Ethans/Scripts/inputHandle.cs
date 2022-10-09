using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputHandle : MonoBehaviour
{

    myTime test;
    public float targetScale;
    public float lerpSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {
        test = myTime.getInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            targetScale = 1;
            lerpSpeed = 10;
        }
        else
        {
            targetScale = 0;
            lerpSpeed = 4;
        }
        test.myTimeScale = Mathf.Lerp(test.myTimeScale, targetScale, Time.deltaTime * lerpSpeed);

    }
}
