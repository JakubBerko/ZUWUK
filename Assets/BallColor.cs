using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColor : MonoBehaviour
{
    public Color[] colorOptions;

    void Awake()
    {
        colorOptions = new Color[]
        {
            Color.red,
            Color.green,
            Color.blue,
            Color.yellow,
        };
    }
    public Color GetRandomColor()
    {
            int randomIndex = Random.Range(0, colorOptions.Length);

            return colorOptions[randomIndex];
    }
}
