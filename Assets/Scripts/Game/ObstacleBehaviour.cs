using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    private int collisionCount = 0;
    private float moveSpeed = 1f;
    private float moveMax = 1f;
    private Vector3 initPos;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.CompareTag("Wall"))
        {
            initPos = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.CompareTag("Wall"))
        {
            float sideMov = Mathf.Sin(Time.time * moveSpeed) * moveMax;
            transform.position = initPos + new Vector3(sideMov, 0f, 0f);
        }
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
        if (gameObject.CompareTag("WaterField"))
        {
            if (collision.gameObject.CompareTag("ShotBall"))
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity *= 0.25f;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.CompareTag("WaterField"))
        {
            if (collision.gameObject.CompareTag("ShotBall"))
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity *= 4f;
            }
        }
    }
}
