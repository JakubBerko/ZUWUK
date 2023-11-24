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

    private GameObject mainBall;
    void Start()
    {
        primBall = Instantiate(ball1Prefab, primaryBallPos.position, Quaternion.identity);
        primBall.transform.parent = transform;
        secBall = Instantiate(ball2Prefab, secondaryBallPos.position, Quaternion.identity);
        secBall.transform.parent = transform;

        mainBall=primBall;
        //nastavit scale (bud mala nebo velka, podle primary nebo secondary)
    }

    void Update()
    {
        RotatePlayer();
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        if (Input.GetMouseButtonDown(1))
        {
            SwapBalls();
        }
        
    }

    private void Shoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mainBall.transform.DOMove(mainBall.transform.position + mainBall.transform.right * 100f, 2f)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                this.
            }); ;
    }

    private void SwapBalls()
    {
        Vector2 tempPosition = primBall.transform.position;
        primBall.transform.position = secBall.transform.position;
        secBall.transform.position = tempPosition;

        mainBall = (mainBall == primBall) ? secBall : primBall;
    }

    private void RotatePlayer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}