using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class Ball : MonoBehaviour
{
    SplineComputer spline;
    private SplineFollower splineFollower;
    void Awake()
    {
    }
    void Start()
    {
        spline = GameObject.Find("Spline").GetComponent<SplineComputer>();
        spline.is2D = true;
        splineFollower = gameObject.AddComponent<SplineFollower>();
        splineFollower.spline = spline;
        splineFollower.motion.is2D = true;
        splineFollower.useTriggers = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
