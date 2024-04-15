using Dreamteck.Splines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallChain : MonoBehaviour
{
    private SplineComputer splineComputer;

    private BallColor ballColor;
    public GameObject ballPrefab;
    [SerializeField] int ballCount = 5;
    private int section = 0;
    private Color ballColorVar;

    void Start()
    {
        SplineComputer splineComputer = GameObject.Find("Spline").GetComponent<SplineComputer>();
        ballColor = GameObject.Find("GameManager").GetComponent<BallColor>();
        ballColorVar = ballColor.GetRandomColor();
        GameObject newBall = CreateBall(ballColorVar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject CreateBall(Color color)
    {
        GameObject ball = Instantiate(ballPrefab,transform.position, Quaternion.identity);
        ball.transform.SetParent(GameObject.Find("Balls").transform);
        ball.AddComponent<Ball>();
        ball.GetComponent<SpriteRenderer>().color = color;
        return ball;
    }
    public void CreateBallOnSpline()
    {
        if (ballCount <= 0)
        {
            return;
        }
        
        GameObject newBall = CreateBall(ballColorVar);
        section--;
        ballCount--;
        if (section <= 0)
        {
            section = UnityEngine.Random.Range(1, 4);
            ballColorVar = ballColor.GetRandomColor();
        }
    }

}
