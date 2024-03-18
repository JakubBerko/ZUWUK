using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    private int collisionCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Wall"))
        {
            if (collision.gameObject.CompareTag("ShotBall"))
            {
                Destroy(collision.gameObject);
                collisionCount++;

                if (collisionCount >= 3)
                {
                    Destroy(gameObject);
                }
            }
        }
        if (gameObject.CompareTag("Tunnel"))
        {
            if (collision.gameObject.CompareTag("ShotBall"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
