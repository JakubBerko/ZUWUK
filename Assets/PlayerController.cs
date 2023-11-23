using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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
        //nastavit scale (bud mala nebo velka, podle primary nebo secondary)
    }

    void Update()
    {
        RotatePlayer();
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            primBall.transform.DOMove(mousePosition, 1f)
                .SetEase(Ease.Linear);
        }
        if (Input.GetMouseButtonDown(1))
        {
            secBall.transform.position = primaryBallPos.position;
            secBall = primBall;
            primBall.transform.position = secondaryBallPos.position;
            primBall = secBall;
        }
    }

    private void RotatePlayer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
