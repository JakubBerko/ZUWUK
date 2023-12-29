using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Transform primaryBallPos;
    public Transform secondaryBallPos;
    public GameObject ball1Prefab;
    public GameObject ball2Prefab;

    private GameObject primBall;
    private GameObject secBall;
    void Start()
    {
        primBall = Instantiate(ball1Prefab, primaryBallPos.position, Quaternion.identity);
        primBall.transform.parent = transform;
        secBall = Instantiate(ball2Prefab, secondaryBallPos.position, Quaternion.identity);
        secBall.transform.parent = transform;
        
    }

    void Update()
    {
        RotatePlayer();
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            MoveSecBallToPrim();
            CreateNewBall();
        }
        if (Input.GetMouseButtonDown(1))
        {
            SwapBalls();
        }
        
    }

    private void MoveSecBallToPrim()
    {
        
    }

    private void CreateNewBall()
    {
        
    }

    private void Shoot()
    {
        primBall.transform.DOMove()
    }


    private void SwapBalls()
    {
        
    }

    private void RotatePlayer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
