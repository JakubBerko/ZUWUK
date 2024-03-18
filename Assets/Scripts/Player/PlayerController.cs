using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public bool isPaused = false;
    private bool canShoot = true;

    private BallColor ballColor;

    float ballMovementSpeed = 300f;

    public Transform primaryBallPos;
    public Transform secondaryBallPos;
    public GameObject ball1Prefab;
    public GameObject PiercingBallPrefab;
    public GameObject CannonBallPrefab;

    private GameObject primBall;
    private GameObject secBall;
    private GameObject tempBall;

    [SerializeField] private Transform spotUp; 
    [SerializeField] private Transform spotDown; 
    private Transform currentPlayerSpot;


    void Start()
    {
        ballColor = GameObject.Find("GameManager").GetComponent<BallColor>();

        primBall = Instantiate(ball1Prefab, primaryBallPos.position, Quaternion.identity);
        primBall.GetComponent<SpriteRenderer>().color = ballColor.GetRandomColor();
        primBall.transform.parent = primaryBallPos;
        primBall.tag = "ShotBall";
        primBall.GetComponent<SpriteRenderer>().sortingLayerName = "Balls";
        primBall.GetComponent<SpriteRenderer>().sortingOrder = 3;
        secBall = Instantiate(ball1Prefab, secondaryBallPos.position, Quaternion.identity);
        secBall.GetComponent<SpriteRenderer>().color = ballColor.GetRandomColor();
        secBall.transform.parent = secondaryBallPos;
        secBall.tag = "ShotBall";
        secBall.GetComponent<SpriteRenderer>().sortingLayerName = "Balls";
        secBall.GetComponent<SpriteRenderer>().sortingOrder = 3;

        currentPlayerSpot = spotDown;
    }

    void Update()
    {
        RotatePlayer();


        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (isPaused) return;
            if (Input.GetMouseButtonDown(1))
            {
                SwapBalls();
            }
            if (Input.GetMouseButtonDown(0) && canShoot)
            {
                Shoot();
                MoveSecBallToPrim();
                CreateNewBall();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwapCharacter();
        }

    }

    private void SwapCharacter()
    {
        Transform destinationSpot = (currentPlayerSpot == spotUp) ? spotDown : spotUp; // If currentPlayerSpot == spotUp, do spotDown = destinationSpot; else, do destinationSpot = spotUp.
        transform.position = destinationSpot.position;
        currentPlayerSpot = destinationSpot;
    }

    private void MoveSecBallToPrim()
    {
        secBall.transform.position = primaryBallPos.position;
        secBall.transform.parent = primaryBallPos;
        primBall = secBall;
        secBall = null;
    }

    private void CreateNewBall() //fix piercing ball (it flickers and doesnt spawn) - ono samo funguje? ok
    {
        int randomChance = UnityEngine.Random.Range(0, 101);
        if (randomChance <=5)
        {
            secBall = Instantiate(PiercingBallPrefab, secondaryBallPos.position, Quaternion.identity);
        }
        else if (randomChance >5 && randomChance <= 10)
        {
            secBall = Instantiate(CannonBallPrefab, secondaryBallPos.position, Quaternion.identity);
        }
        else
        {
            secBall = Instantiate(ball1Prefab, secondaryBallPos.position, Quaternion.identity);
            secBall.GetComponent<SpriteRenderer>().color = ballColor.GetRandomColor();
            
        }
        secBall.transform.parent = secondaryBallPos;
        secBall.tag = "ShotBall";
        secBall.GetComponent<SpriteRenderer>().sortingLayerName = "Balls";
        secBall.GetComponent<SpriteRenderer>().sortingOrder = 3;
    }
    
    private void Shoot()
    {
        Vector2 shootingDirection = transform.right;

        primBall.transform.parent = null;
        primBall.GetComponent<Rigidbody2D>().AddForce(shootingDirection * ballMovementSpeed);

        StartCoroutine(DespawnBallAfterDelay(primBall, 2f));
        StartCoroutine(ShotLimiter());

    }
    IEnumerator ShotLimiter() //antispam støel, zruš støíení, poèkej nìjaký èas, povol støílení
    {
        canShoot = false;
        yield return new WaitForSeconds(0.2f);
        canShoot = true;
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

    private void RotatePlayer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private IEnumerator DespawnBallAfterDelay(GameObject ball, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (ball != null)
        {
            Destroy(ball);
        }
    }
}
