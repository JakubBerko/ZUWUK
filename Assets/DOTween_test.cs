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
    private GameObject testObject;

    void Start()
    {
        testObject = Instantiate(prefabToSpawn, spawnPos.transform.position, Quaternion.identity);
        CreatePath();
    }

    private void CreatePath()
    {
        testObject.transform.DOPath(waypoints, 3f, PathType.CatmullRom);
    }
}
