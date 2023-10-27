using DG.Tweening;
using DG.Tweening.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOTween_test : MonoBehaviour
{
    public GameObject prefabToSpawn;
    
    public Transform spawnPos;
    public Vector3[] waypoints = new Vector3[3];
    public int numberOfBalls = 10;
    public float spawnDelay = 0.5f;
    public GameObject[] balls;

    void Start()
    {
        balls = new GameObject[numberOfBalls];
        for (int i = 0; i < numberOfBalls; i++)
        {
            GameObject obj = Instantiate(prefabToSpawn, spawnPos.transform.position, Quaternion.identity);
            balls[i] = obj;

            obj.transform.DOLocalMove(Vector2.zero, 0.01f); 
            obj.transform.DOPause(); 
        }
        
        //testObject = Instantiate(prefabToSpawn, spawnPos.transform.position, Quaternion.identity);
        CreatePath();

        //StartCoroutine(SpawnChain());
    }

    private void CreatePath()
    {
        balls[i].transform.DOPath(waypoints, 3f, PathType.CatmullRom);
    }

    public IEnumerator SpawnChain()
    {
        for (int i = 0; i < numberOfLinks; i++)
        {
            //spawnedObject = Instantiate(prefabToSpawn, spawnPos.transform.position, Quaternion.identity);

            //spawnedObject.transform.DOMove(endPos.transform.position, 1f).SetEase(Ease.Linear);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
