using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    private ModInv mI;
    public GameObject modPrefab;
    private CardManager cm;

	// Use this for initialization
	private void Start () {
        mI = GameObject.FindGameObjectWithTag("Player").GetComponent<ModInv>();
        cm = CardManager.cm;
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            for (int i = 0; i < mI.modSlots.Length; i++)
            {
                if(mI.isFull[i]==false)
                {
                    mI.isFull[i] = true;
                    Instantiate(modPrefab, mI.modSlots[i].transform, false);
                    cm.dub = true;
                    Destroy(gameObject);
                    break; 
                }

            }

        }
    }
}
