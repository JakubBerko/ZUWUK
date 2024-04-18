using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleBehaviours : MonoBehaviour
{
    private ScoreManager scoreManager;

    //anim
    public float wiggleDuration = 1f;
    public float wiggleStrength = 0.2f;
    public int wiggleVibrato = 5;
    public float wiggleRandomness = 90f;
    public float moveDuration = 1f;
    public float moveDistance = 1f;
    private Vector3 startPosition;

    private void Start()
    {
        scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
    }
    
    public void CollectibleDo()
    {
        if (gameObject.CompareTag("Coin"))
        {
            scoreManager.AddNumScore(20);
            scoreManager.AddSliderScore(20f);

            Debug.Log("Coin hit!" + gameObject.name);
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Secret"))
        {
            Debug.Log("Hit secret!");
            scoreManager.AddNumScore(500);
            gameObject.GetComponent<SpriteRenderer>().DOFade(0, 1f).OnComplete(DestroySecret);
        }
        else if (gameObject.CompareTag("SecretObject"))
        {
            Debug.Log("Hit secret object!");
            startPosition = transform.position;

            //pt·Ëkova animace 
            DG.Tweening.Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOLocalMoveX(startPosition.x + wiggleStrength, wiggleDuration).SetEase(Ease.InOutQuad).SetRelative(true).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad));
            sequence.Join(transform.DOLocalRotate(new Vector3(0, 0, wiggleRandomness), wiggleDuration).SetRelative(true).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad));
            sequence.Append(transform.DOLocalMoveY(startPosition.y + moveDistance, moveDuration).SetEase(Ease.InOutQuad).SetRelative(true));
        }
    }

    private void DestroySecret()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ShotBall")
        {
            CollectibleDo();

            Destroy(collision.gameObject);
        }
    }
}
