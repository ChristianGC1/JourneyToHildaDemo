using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    public float speed;

    private Animator animator;
    public GameObject bloodEffect;


    void Start()
    {
       // animator = GetComponent<Animator>();
      //  animator.SetBool("isRunning", true);
    }

    void Update()
    {
        if(health <= 0)
        {
            
            CoinCount.count += 10; 
            Destroy(gameObject);
        }

        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        //Instantiate(bloodEffect, transform.position, Quaternion.identity);
        gameObject.GetComponent<AudioSource>().Play();
        health -= damage;
        Debug.Log("damage TAKEN");
    }
}
