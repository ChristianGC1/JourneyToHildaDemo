using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChar : MonoBehaviour
{
    public int health;
    public Transform target;
    public float speed;
    public GameObject bloodEffect;

    private Rigidbody2D rB;
    private Vector2 movement;
    private Animator animator;
    //private GameObject player;


    void Start()
    {
        rB = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //animator.SetBool("isRunning", true);

        //player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 direction = target.position - transform.position;


        if (target.position.x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        direction.Normalize();
        movement = direction;

        if (health <= 0)
        {            
            CoinCount.count += 10; 
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        rB.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    public void TakeDamage(int damage)
    {
        //Instantiate(bloodEffect, transform.position, Quaternion.identity);
        gameObject.GetComponent<AudioSource>().Play();
        health -= damage;
        Debug.Log("damage TAKEN");
    }

}