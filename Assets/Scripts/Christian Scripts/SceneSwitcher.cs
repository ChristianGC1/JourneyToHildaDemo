using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string myScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CoinCount.count >= 1000)
        {
            Debug.Log("Entered portal!");
            SceneManager.LoadScene(myScene);
        }        
    }
}