using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesDead : MonoBehaviour
{
    void Update()
    {
        if (GameObject.Find("Enemy") == false)
        {
            Debug.Log("ALL ENEMIES ARE DEAD !!!!!!!!!!!!!!!!!");
        }
    }
}
