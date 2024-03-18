using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    //Spline spline;


    [SerializeField] SplineComputer splineComputer;
    public List<GameObject> balls = new List<GameObject>();
    public GameObject shotBall;
    
    // Start is called before the first frame update
    void Start()
    {
        //spline.EvaluatePosition()


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddBallToList(GameObject ball)
    {
        balls.Add(ball);
    }
}
