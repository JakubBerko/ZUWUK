using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShotBehaviour : MonoBehaviour
{
    private bool cannotCollide = false;
    private void Awake()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (cannotCollide) return;
        if (collider.gameObject.tag != "Ball") return;
        cannotCollide = true;
        Ball ball = collider.gameObject.GetComponent<Ball>();
        ball.MoveOnSpline();
        double ballPos = collider.gameObject.GetComponent<SplineFollower>().GetPercent();
        Debug.Log($"BallPos: {ballPos}");
        if (GetComponent<Ball>() == null)
        {
            gameObject.AddComponent<Ball>();

            gameObject.GetComponent<Ball>().PlaceInLine(ballPos);
            Debug.Log("Placing in line to: " + ballPos);
        }

        Destroy(this);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
