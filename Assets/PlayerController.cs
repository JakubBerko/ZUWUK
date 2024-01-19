using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public bool isPaused = false;

    private BallColor ballColor;

    float ballMovementSpeed = 300f;

    public Transform primaryBallPos;
    public Transform secondaryBallPos;
    public GameObject ball1Prefab;

    private GameObject primBall;
    private GameObject secBall;
    private GameObject tempBall;
    void Start()
    {
        ballColor = GameObject.Find("GameManager").GetComponent<BallColor>();

        primBall = Instantiate(ball1Prefab, primaryBallPos.position, Quaternion.identity);
        primBall.GetComponent<SpriteRenderer>().color = ballColor.GetRandomColor();
        primBall.transform.parent = primaryBallPos;
        secBall = Instantiate(ball1Prefab, secondaryBallPos.position, Quaternion.identity);
        secBall.GetComponent<SpriteRenderer>().color = ballColor.GetRandomColor();
        secBall.transform.parent = secondaryBallPos;
        
    }

    void Update()
    {
        RotatePlayer();

        if (isPaused) return;
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
        secBall.transform.position = primaryBallPos.position;
        secBall.transform.parent = primaryBallPos;
        primBall = secBall;
        secBall = null;
    }

    private void CreateNewBall()
    {
            secBall = Instantiate(ball1Prefab, secondaryBallPos.position, Quaternion.identity);
            secBall.transform.parent = secondaryBallPos;
            secBall.GetComponent<SpriteRenderer>().color = ballColor.GetRandomColor();
    }

    private void Shoot()
    {
        Vector2 shootingDirection = transform.right;

        primBall.transform.parent = null;
        primBall.GetComponent<Rigidbody2D>().AddForce(shootingDirection * ballMovementSpeed);
    }


    private void SwapBalls()
    {
        primBall.transform.position = secondaryBallPos.position;
        secBall.transform.position = primaryBallPos.position;
        //primBall.transform.DOMove(secondaryBallPos.position, 0.5f);
        //secBall.transform.DOMove(primaryBallPos.position, 0.5f);

        tempBall = primBall;
        primBall = secBall;
        secBall = tempBall;
        tempBall = null;
    }
    /*
    private void KillSwapTween() //mozna zbytecne
    {
        DOTween.Kill("swapBalls");
    } 
    */

    private void RotatePlayer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
