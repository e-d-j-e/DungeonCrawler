using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    private static int enemiesKilled = 0;
    public int enemiesKilledReq = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.name == "basic 1(Clone)")
        {
            if(enemiesKilled >= enemiesKilledReq)
            Destroy(gameObject);
        }
    }
    public static void KilledEnemies()
    {
        enemiesKilled++;
    }
}
