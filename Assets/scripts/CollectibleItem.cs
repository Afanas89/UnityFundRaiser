using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public GameEngine ge;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (this.tag == "CollectItems")
            {
                GameEngine.AddMoney(1);
                Destroy(this.gameObject);
            }
            else if (this.tag == "finishPoint")
            {
                ge.ShowGameOver(true);
                
            }


        }
        
    }
}
