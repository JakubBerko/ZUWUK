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
    private GameObject front = null;
    private GameObject ahead = null;
    private int section = 0;


    void Start()
    {
        ballColor = GameObject.Find("GameManager").GetComponent<BallColor>();
        List<GameObject> balls = new List<GameObject>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ballCount <=0)
        {
            return;
        }
        GameObject newBall = CreateBall();
        SetAssoc(newBall, ahead);
        front = newBall;
        section--;
        ballCount--;
        Debug.Log(ballCount);
        if (section <= 0)
        {
            section = UnityEngine.Random.Range(1, 4);
        }
    }

    private void SetAssociation(List<GameObject> balls)
    {
        GameObject behind = null;

        for (int i = 0; i < balls.Count; i++)
        {
            //if (behind != null) balls[i].GetComponent<Ball>().AdjustRespectiveLocation(behind, true); //TODO set the location

            behind = balls[i];
        }
    }

    public GameObject CreateBall()
    {
        GameObject ball = Instantiate(ballPrefab,transform.position, Quaternion.identity);
        ball.transform.parent = transform;
        ball.GetComponent<SpriteRenderer>().color = ballColor.GetRandomColor();
        ball.AddComponent<Ball>();

        return ball;
    }
    private static void SetAssoc(GameObject behind, GameObject ahead)
    {
        //if (ahead != null) ahead.GetComponent<Ball>().behind = behind;
        //if (behind != null) behind.GetComponent<Ball>().ahead = ahead;
    }
}
