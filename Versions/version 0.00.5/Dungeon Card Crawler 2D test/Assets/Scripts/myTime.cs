using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myTime : MonoBehaviour
{
    public float myDelta;
    public float myFixedDelta;
    public float myTimeScale = 1;

    public static myTime instance;

    public static myTime getInstance()
    {
        return instance;
    }

    void awake()
    {
        instance = this;
    }
    void FixedUpdate()
    {
        myFixedDelta = Time.fixedDeltaTime * myTimeScale;
    }

    // Update is called once per frame
    void Update()
    {
        myDelta = Time.deltaTime * myTimeScale;
    }
}
