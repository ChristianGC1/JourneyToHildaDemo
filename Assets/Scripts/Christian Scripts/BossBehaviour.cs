using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] private int health;

    [SerializeField] private GameObject floatingTextPrefab;

    public int goldDrop;

    public Rigidbody2D rB;

    public Animator animator;

    public GameObject spawner;

    public GameObject spawnerTwo;

    private IAstarAI ai;

    private int admg = 3;

    void Start()
    {
        rB = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        ai = GetComponent<IAstarAI>();
    }

    private void Update()
    {
        ai.destination = GameObject.FindGameObjectWithTag("Player").transform.position;

        GetComponent<SpriteRenderer>().flipX = rB.velocity.x < 0;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Hurt(int damage)
    {

        ShowNumber(damage.ToString());

        gameObject.GetComponent<AudioSource>().Play();

        health -= damage;

        Debug.Log("damage TAKEN");
    }

    void Die()
    {
        CoinCount.count += goldDrop;
        Destroy(spawner.gameObject);
        Destroy(spawnerTwo.gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            ShowNumber(admg.ToString());

            gameObject.GetComponent<AudioSource>().Play();
            health -= 3;
            Debug.Log("damage TAKEN");
        }
    }

    void ShowNumber(string text)
    {
        if (floatingTextPrefab)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }
}
