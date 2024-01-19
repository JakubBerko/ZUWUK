using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour
{
    [SerializeField] Transform[] ballPositions;
    [SerializeField] GameObject coinPrefab;

    private void Start()
    {
        StartCoroutine(SpawnCoin());
    }
    private IEnumerator SpawnCoin()
    {
        while (true) 
        {
            int randomIndex = Random.Range(0, ballPositions.Length);
            Transform randPosition = ballPositions[randomIndex];
            //Debug.Log(randPosition.position);
            GameObject coin = Instantiate(coinPrefab, randPosition.position, Quaternion.identity);
            StartCoroutine(DespawnCoin(coin, 2f));
            yield return new WaitForSeconds(3f);
        }
        
    }
    private IEnumerator DespawnCoin(GameObject coin, float time)
    {
        yield return new WaitForSeconds(time);
        if (coin != null)
        {
            Destroy(coin);
        }
    }

}
