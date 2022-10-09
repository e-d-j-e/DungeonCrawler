using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    private static int enemiesKilled = 0;
    public int enemiesKilledReq;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesKilled >= enemiesKilledReq)
        {
            Destroy(gameObject);
        }
    }
   
    public static void KilledEnemies()
    {
        enemiesKilled++;
    }
}
