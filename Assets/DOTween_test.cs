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
    private GameObject testObject;

    void Start()
    {
        testObject = Instantiate(prefabToSpawn, spawnPos.transform.position, Quaternion.identity);
        CreatePath();
    }

    private void CreatePath()
    {

    }
}
