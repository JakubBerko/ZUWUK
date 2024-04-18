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

    public GameObject ahead;
    public GameObject behind;
    public Action<Ball> OnBallReady;

    private float offset = 0.3f;
    void Awake()
    {
        
    }
    private void OnEnable()
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
        gameObject.layer = 10; //layer 10 is ball layer
        

        gameObject.tag = "Ball";

        FindNeighboringBalls();
        StartCoroutine(Bolest());
        //PlaceInLine(0.66);
    }
    IEnumerator Bolest()
    {
        yield return new WaitForSeconds(0.1f);
        OnBallReady?.Invoke(this);

    }
    // Update is called once per frame
    void Update()
    {
    }
    //public Vector3 GetPositionOnSpline() //touhle cesou to nepùjde
    //{
    //    Vector3 positionOnSpline = splineFollower.result.position;
    //    return positionOnSpline;
    //}
    public void MoveOnSpline()
    {

        //TODO: assign splinefollower(done at start), add position on spline, add ball to list, (maybe remove its speed if needed? -> do this in ball script so it is a ball not shot ball)
        Debug.Log($"Moving ball {GetIndexFromBalls()}");

        splineFollower.Move(offset);
        ahead?.GetComponent<Ball>().MoveOnSpline();
    }
    private void FindNeighboringBalls() //ahead = koule pøed ; behind = koule za
    {
        List<GameObject> balls = BallBehaviour.GetBalls();
        int index = GetIndexFromBalls();

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

        int aheadIndex = ahead == null ? -1 : ahead.GetComponent<Ball>().GetIndexFromBalls();
        int behindIndex = behind == null ? -1 : behind.GetComponent<Ball>().GetIndexFromBalls();

        Debug.Log($"Index: {index}\nAhead: {aheadIndex}\nBehind: {behindIndex}");



        // // najít koule a dát jim index (záznam) v listu -> nefunguje, všechny koule mají stejný index ahead a behind neexistuje
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
    public int GetIndexFromBalls()
    {
        return BallBehaviour.GetBalls().IndexOf(gameObject);
    }
    public void MoveBackCombo()
    {
        StartCoroutine(MoveBack(1.5f));
    }
    private IEnumerator MoveBack(float speed)
    {
        splineFollower.direction = Spline.Direction.Backward;
        splineFollower.followSpeed = -speed;
        Debug.Log("Moving back" + splineFollower.direction);
        yield return new WaitForSeconds(2f);
        splineFollower.direction = Spline.Direction.Forward;
        splineFollower.followSpeed = 1f;
    }
    public void PlaceInLine(double percent)
    {
        Debug.Log("pøed if");
        if (splineFollower != null)
        {
            Debug.Log("pøed kodem");
            splineFollower.Move(percent);
            //splineFollower.Move(1);
            Debug.Log("Placing in line to: " + percent);
        }
        else
        {
            Debug.LogError("SplineFollower component not initialized.");
        }
    }

    
}

























/*
    public void FindOffset()
    {
        double percentA = splineFollower.GetPercent();
        double percentB = ahead.GetComponent<SplineFollower>().GetPercent();

        Vector3 positionA = GetPositionOnSpline();
        Vector3 positionB = ahead.GetComponent<Ball>().GetPositionOnSpline();

        double distance = (Vector3.Distance(positionA, positionB))/2;//vyjde 0.3 a vloženo do offsetu tudíž je fce nevyužita
        Debug.Log("Distance: " + distance);
    } 
    */