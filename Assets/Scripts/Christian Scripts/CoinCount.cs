using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCount : MonoBehaviour
{
    public static int count;
    public static int pot;
    public Text gold;
    public Text potion;

    // Start is called before the first frame update
    void Start()
    {
        count = PlayerPrefs.GetInt("amount");
        pot = PlayerPrefs.GetInt("hMany");
    }

    // Update is called once per frame
    void Update()
    {
        gold.text = " " + count;
        potion.text = " " + pot;
    }
}
