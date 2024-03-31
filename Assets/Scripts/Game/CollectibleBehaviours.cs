using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleBehaviours : MonoBehaviour
{
    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
    }
    
    public void CollectibleDo()
    {
        if (gameObject.CompareTag("Coin"))
        {
            scoreManager.AddNumScore(20);
            scoreManager.AddSliderScore(20f);

            //Debug.Log("Coin hit!");
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Secret"))
        {
            Debug.Log("Hit secret!");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ShotBall")
        {
            CollectibleDo();
            Destroy(collision.gameObject);
        }
    }
}
