using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Unity.VisualScripting;

public class Ball : MonoBehaviour
{
    SplineComputer spline;

    ScoreManager scoreManager;

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

        scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();

        spline.is2D = true;
        splineFollower = gameObject.AddComponent<SplineFollower>();

        splineFollower.spline = spline;
        splineFollower.motion.is2D = true;
        splineFollower.useTriggers = true;
        gameObject.layer = 10; //layer 10 is ball layer
        

        gameObject.tag = "Ball";

        FindNeighboringBalls();
        StartCoroutine(Bolest());
    }
    IEnumerator Bolest()
    {
        yield return new WaitForSeconds(0.1f);
        OnBallReady?.Invoke(this);

    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Findbalz());
    }
    IEnumerator Findbalz()
    {
        yield return new WaitForSeconds(0.1f);
        FindNeighboringBalls();
    }
    //public Vector3 GetPositionOnSpline() //touhle cesou to nep�jde
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
    public void FindNeighboringBalls() //ahead = koule p�ed ; behind = koule za
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

        //Debug.Log($"Index: {index}\nAhead: {aheadIndex}\nBehind: {behindIndex}");



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
    public bool FindAndDestroyNeighborWithSameColorAndSelf(Color color)
    {
        Color aheadCol = ahead != null ? ahead.GetComponent<Ball>().GetColor(): Color.clear; 
        Color behindCol = behind != null ? behind.GetComponent<Ball>().GetColor() : Color.clear;
        if (ahead != null && ahead.GetComponent<Ball>().GetColor() == GetComponent<SpriteRenderer>().color && color == GetComponent<SpriteRenderer>().color)
        {
            Debug.Log("Same color ahead");
            Destroy(ahead);
            scoreManager.AddNumScore(200);
            return true;
        }
        if (behind != null && behind.GetComponent<Ball>().GetColor() == GetComponent<SpriteRenderer>().color && color == GetComponent<SpriteRenderer>().color)
        {
            Debug.Log("Same color behind");
            Destroy(behind);
            scoreManager.AddNumScore(200);
            return true;
        }
        if(aheadCol == GetComponent<SpriteRenderer>().color && behindCol == GetComponent<SpriteRenderer>().color && color == GetComponent<SpriteRenderer>().color)
        {
            Debug.Log("Same color ahead and behind");
            Destroy(gameObject);
            scoreManager.AddNumScore(200);
            return true;
        }
        return false;
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
        //Debug.Log("p�ed if");
        if (splineFollower != null)
        {
            //Debug.Log("p�ed kodem");
            splineFollower.Move(percent - 0.0045);
            //splineFollower.Move(1);
            Debug.Log("Placing in line to: " + percent);
        }
        else
        {
            Debug.LogError("SplineFollower component not initialized.");
        }
    }
    public Color GetColor()
    {
        return GetComponent<SpriteRenderer>().color;
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