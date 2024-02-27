using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShotBehaviour : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        CollectibleBehaviours collectibleBehaviours = collider.GetComponent<CollectibleBehaviours>();

        if (collectibleBehaviours != null)
        {
            collectibleBehaviours.CollectibleDo();
            Destroy(gameObject);
        }
    }
}
