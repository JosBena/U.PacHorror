using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoins : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTags.coin))
        {
            print("collect coin");
            Destroy(other.gameObject);
            Scriptables.PlayerScore.coins += 1;
        }
    }
}
