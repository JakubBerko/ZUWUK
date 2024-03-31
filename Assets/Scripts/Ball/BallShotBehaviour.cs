using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShotBehaviour : MonoBehaviour
{
    private void Awake()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ball")
        {
            Ball ball = collider.gameObject.GetComponent<Ball>();
            Vector3 positionOnSpline = ball.GetPositionOnSpline();
            Debug.Log("Hit ball's position: " + positionOnSpline);
            gameObject.AddComponent<Ball>();
        }
    }

}
