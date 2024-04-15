using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public static List<GameObject> GetBalls()
    {
        List<GameObject> balls = new List<GameObject>();
        foreach (Transform trans in GameObject.Find("Balls").transform) balls.Add(trans.gameObject);
        return balls;
    }
}
