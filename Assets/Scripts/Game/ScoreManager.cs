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
    [SerializeField] TMP_Text livesText;

    private int totalScore;
    private int scoreToAdd;

    private float totalValue;

    private int lives = 1; //TODO save lives and load them


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
        if(totalValue >= 100)
        {
            return;
        }
        totalValue = scoreSlider.value + scorePercent;
        scoreSlider.DOValue(totalValue, 1.5f)
            .SetEase(Ease.InQuad);
        if(totalValue >=100)
        UpdateLives();
        
    }
    public void SetScoreAtStart()
    {
        scoreText.text = "0";
    }
    private void UpdateLives()
    {
        livesText.text = "x" + (lives+1);
    }
}
