using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Listener"))
        {
            Enemy enemy = col.GetComponent<Enemy>();
            enemy.FoundSomething(this.gameObject);
        }
    }
}
