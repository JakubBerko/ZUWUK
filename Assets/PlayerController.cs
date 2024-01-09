using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private BallColor ballColor;

    bool shootPrimary = true;
    float ballMovementSpeed = 300f;

    public Transform primaryBallPos;
    public Transform secondaryBallPos;
    public GameObject ball1Prefab;
    public GameObject ball2Prefab;

    private GameObject primBall;
    private GameObject secBall;
    void Start()
    {
        ballColor = GameObject.Find("GameManager").GetComponent<BallColor>();

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
            CreateNewBall();
        }
        if (Input.GetMouseButtonDown(1))
        {
            SwapBalls();
        }

    }

    private void MoveSecBallToPrim()
    {
        //TODO
    }

    private void CreateNewBall()
    {
            primBall = Instantiate(ball1Prefab, primaryBallPos.position, Quaternion.identity);
            primBall.transform.parent = transform;
            primBall.GetComponent<SpriteRenderer>().color = ballColor.GetRandomColor();
    }

    private void Shoot()
    {
        Vector2 shootingDirection = transform.right;

        if (shootPrimary == true)
        {
            primBall.transform.parent = null;
            primBall.GetComponent<Rigidbody2D>().AddForce(shootingDirection * ballMovementSpeed);
        }
        else
        {
            secBall.transform.parent = null;
            secBall.GetComponent<Rigidbody2D>().AddForce(shootingDirection * ballMovementSpeed);
        }
    }


    private void SwapBalls()
    {
        shootPrimary = !shootPrimary;

        Vector2 primBallPos = primaryBallPos.position;
        Vector2 secBallPos = secondaryBallPos.position;

        DOTween.Sequence()
            .Append(primBall.transform.DOMove(secBallPos, 0.2f))
            .Join(secBall.transform.DOMove(primBallPos, 0.2f))
            .SetId("swapBallsT")
            .OnComplete(() => KillSwapTween()); 
    }

    private void KillSwapTween() //mo�n� zbyte�n�
    {
        DOTween.Kill("swapBalls");
    }

    private void RotatePlayer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
