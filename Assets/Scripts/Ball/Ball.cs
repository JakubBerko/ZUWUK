using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ballRadius = 0.35f;
    public GameObject ahead = null;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AdjustRespectiveLocation(GameObject referenceBall, bool behind)
    {
        float offset = (behind) ? -ballRadius : ballRadius; 
        Vector3 newPosition = referenceBall.transform.position + new Vector3(0f, offset, 0f);
        transform.position = newPosition;
    }
}
