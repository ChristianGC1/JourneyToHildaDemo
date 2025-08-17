using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RapidFire : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CoinCount.count >= 500)
        {
            CoinCount.count -= 500;

            PlayerPrefs.SetInt("amount", CoinCount.count);
            ShootBow.bowCooldown = 0.25f;

            Destroy(gameObject);
        }
    }
}
