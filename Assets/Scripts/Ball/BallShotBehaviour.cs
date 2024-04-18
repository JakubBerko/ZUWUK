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
        if (gameObject.CompareTag("PiercingBall"))
        {
            if (collider.gameObject.CompareTag("Ball"))
            {
                Destroy(collider.gameObject);
            }   
        }
        if (cannotCollide) return;
        if (collider.gameObject.tag != "Ball") return;
        cannotCollide = true;
        Ball ball = collider.gameObject.GetComponent<Ball>();
        ball.GetColor();
        ball.MoveOnSpline();
        double ballPos = collider.gameObject.GetComponent<SplineFollower>().GetPercent();
        Debug.Log($"BallPos: {ballPos}");
        if (GetComponent<Ball>() == null)
        {
            Ball createdBall = gameObject.AddComponent<Ball>();

            //createdBall.PlaceInLine(ballPos);
            gameObject.transform.parent = GameObject.Find("Balls").transform;
            createdBall.OnBallReady += (ballsssss) => { ballsssss.PlaceInLine(ballPos); };
            Debug.Log("Placing in line to: " + ballPos);
        }
        Debug.Log("Is the ball the same color?: "+ball.FindAndDestroyNeighborWithSameColorAndSelf(GetColor()));
        if(ball.FindAndDestroyNeighborWithSameColorAndSelf(GetColor()))
        {
            Destroy(gameObject);
        }
        
        Destroy(this);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    public Color GetColor()
    {
        return GetComponent<SpriteRenderer>().color;
    }
}
