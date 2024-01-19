using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Slider scoreSlider;

    private int totalScore;
    private int scoreToAdd;

    private float totalValue;


    private void Start()
    {
        SetScoreAtStart();
    }
    
    public void AddNumScore(int scoreNum)
    {
        totalScore = int.Parse(scoreText.text);
        scoreToAdd = scoreNum;
        totalScore += scoreToAdd;
        scoreText.text = totalScore.ToString();
    }
    public void AddSliderScore(float scorePercent) //už je v procentech
    {
        totalValue = scoreSlider.value + scorePercent;
        scoreSlider.DOValue(totalValue, 1.5f)
            .SetEase(Ease.InQuad);
    }
    public void SetScoreAtStart()
    {
        scoreText.text = "0";
    }
}
