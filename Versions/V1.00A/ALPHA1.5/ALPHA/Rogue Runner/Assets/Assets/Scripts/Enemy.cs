using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public GameObject proj;
	// Use this for initialization
	void Start () {
        StartCoroutine("Fire");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            Instantiate(proj, transform.position, Quaternion.identity);
            
        }
    }
}
