using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallChain : MonoBehaviour
{
    //spline.EvaluatePosition();

    private BallColor ballColor;
    public GameObject ballPrefab;
    [SerializeField] int ballCount = 5;
    private GameObject ahead = null;
    private int section = 0;
    private Color ballColorVar;

    void Start()
    {
        ballColor = GameObject.Find("GameManager").GetComponent<BallColor>();
        List<GameObject> balls = new List<GameObject>();
        ballColorVar = ballColor.GetRandomColor();
    }

    // Update is called once per frame
    void Update()
    {
        if(ballCount <= 0)
        {
            return;
        }
        GameObject newBall = CreateBall(ballColorVar);
        SetAssoc(newBall, ahead);
        ahead = newBall;
        section--;
        ballCount--;
        if (section <= 0)
        {
            section = UnityEngine.Random.Range(1, 4);
            ballColorVar = ballColor.GetRandomColor();
        }
    }

    private void SetAssociation(GameObject newBall, GameObject behind)
    {
        if (behind != null)
        {
            newBall.GetComponent<Ball>().AdjustRespectiveLocation(behind, true);
        }
    }

    public GameObject CreateBall(Color color)
    {
        GameObject ball = Instantiate(ballPrefab,transform.position, Quaternion.identity);
        ball.transform.parent = transform;
        ball.AddComponent<Ball>();
        ball.GetComponent<SpriteRenderer>().color = color;
        return ball;
    }

    private static void SetAssoc(GameObject behind, GameObject ahead)
    {
        if (ahead != null) ahead.GetComponent<Ball>().AdjustRespectiveLocation(behind,false);
        if (behind != null) behind.GetComponent<Ball>().ahead = ahead;
    }
}
