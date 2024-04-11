using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Unity.VisualScripting;

public class Ball : MonoBehaviour
{
    SplineComputer spline;
    private SplineFollower splineFollower;
    private BallBehaviour ballBehaviour;

    public GameObject ahead;
    public GameObject behind;

    private float offset = 0.3f;
    void Awake()
    {
    }
    void Start()
    {
        ballBehaviour = GameObject.Find("GameManager").GetComponent<BallBehaviour>();
        spline = GameObject.Find("Spline").GetComponent<SplineComputer>();

        spline.is2D = true;
        splineFollower = gameObject.AddComponent<SplineFollower>();
        splineFollower.spline = spline;
        splineFollower.motion.is2D = true;
        splineFollower.useTriggers = true;
        gameObject.layer = 10; //layer 10 is ball layer
        Debug.Log(ballBehaviour.balls.Count);
        

        gameObject.tag = "Ball";

        FindNeighboringBalls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 GetPositionOnSpline()
    {
        Vector3 positionOnSpline = splineFollower.result.position;
        return positionOnSpline;
    }
    public void MoveOnSpline()
    {
        //TODO: assign splinefollower(done at start), add position on spline, add ball to list, (maybe remove its speed if needed? -> do this in ball script so it is a ball not shot ball)

        Debug.Log("Moving on spline");

        splineFollower.Move(offset);
        //ahead.GetComponent<Ball>().MoveOnSpline(); -- lag machine memory leak a co je�t� v�c, nekone�n� loop vol�n� na move
    }
    private void FindNeighboringBalls() //ahead = koule p�ed ; behind = koule za
    {
        List<GameObject> balls = ballBehaviour.balls;
        int index = balls.IndexOf(gameObject)+1;
        Debug.Log("Index: " + index);

        // Set behind ball
        if (index < balls.Count - 1)
            behind = balls[index + 1];
        else
            behind = null; // last ball

        // Set ahead ball
        if (index > 0)
            ahead = balls[index - 1];
        else
            ahead = null; // first ball

        Debug.Log("Behind: " + behind + " Ahead: " + ahead);



        // // naj�t koule a d�t jim index (z�znam) v listu -> nefunguje, v�echny koule maj� stejn� index ahead a behind neexistuje
        //GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        //Array.Sort(balls, (x, y) => {
        //    float distX = Vector3.Distance(transform.position, x.transform.position);
        //    float distY = Vector3.Distance(transform.position, y.transform.position);
        //    return distX.CompareTo(distY);
        //});

        //int index = Array.IndexOf(balls, gameObject);

        //if (index > 0)
        //    behind = balls[index - 1];
        //if (index < balls.Length - 1)
        //    ahead = balls[index + 1];
        //Debug.Log("Behind: " + behind + " Ahead: " + ahead);
    }

    public void LostGameMoveFast()
    {
        splineFollower.followSpeed = 20f;
    }
}

























/*
    public void FindOffset()
    {
        double percentA = splineFollower.GetPercent();
        double percentB = ahead.GetComponent<SplineFollower>().GetPercent();

        Vector3 positionA = GetPositionOnSpline();
        Vector3 positionB = ahead.GetComponent<Ball>().GetPositionOnSpline();

        double distance = (Vector3.Distance(positionA, positionB))/2;//vyjde 0.3 a vlo�eno do offsetu tud� je fce nevyu�ita
        Debug.Log("Distance: " + distance);
    } 
    */