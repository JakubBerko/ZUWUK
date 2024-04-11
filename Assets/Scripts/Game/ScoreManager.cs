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
    [SerializeField] TMP_Text lifesText;

    private int totalScore;
    private int scoreToAdd;

    private float totalValue;

    public int lifes;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Lifes"))
        {
            lifes = PlayerPrefs.GetInt("Lifes");
        }
        SetScoreAtStart();
        UpdateLifes();
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
        AddLife();
        
    }
    public void SetScoreAtStart()
    {
        scoreText.text = "0";
    }
    public void UpdateLifes()
    {
        lifesText.text = "x" + (lifes);
        PlayerPrefs.SetInt("Lifes", lifes);
        PlayerPrefs.Save();
    }
    public void RemoveLife()
    {
        lifes--;
        lifesText.text = "x" + (lifes);
        PlayerPrefs.SetInt("Lifes", lifes);
        PlayerPrefs.Save();
    }
    private void AddLife()
    {
        lifes++;
        lifesText.text = "x" + (lifes);
        PlayerPrefs.SetInt("Lifes", lifes);
        PlayerPrefs.Save();
    }

}
