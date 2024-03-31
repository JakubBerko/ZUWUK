using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class Ball : MonoBehaviour
{
    SplineComputer spline;
    private SplineFollower splineFollower;
    private BallBehaviour ballBehaviour;
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
        gameObject.tag = "Ball";
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
    private void MoveOnSpline()
    {
        //TODO: assign splinefollower(done at start), add position on spline, add ball to list, (maybe remove its speed if needed? -> do this in ball script so it is a ball not shot ball)
        
    }
}
