using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController: MonoBehaviour 
{
    [SerializeField] private GameObject floatingTextPrefab;

    private Rigidbody2D playerRigidbody;

    private Animator playerAnimator;

    private SpriteRenderer playerSprite;

    private Vector2 move;

    private float lookX;

    private Transform mouse;

    private Transform attackPos;

    private int gd = 25;

    public int speed;

    public Image bowImage;

    public string gameOver;

    [Header("Health Variables")]

    public int maxHealth = 25;

    public int currentHealth;

    public HealthBar healthBar;
        
    void Awake()
    {
        // grab components from player object
        playerRigidbody = GetComponent<Rigidbody2D>();

        playerAnimator = GetComponent<Animator>();

        playerSprite = GetComponent<SpriteRenderer>();

        mouse = GameObject.Find("MouseLook").transform;

        attackPos = GameObject.Find("AttackPos").transform;
    }

    void FixedUpdate()
    {
        // check if an Attack animation is currently playing
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            // functionally disable move input to prevent moving while the player attacks
            move = Vector2.zero;
        }
        else
        {
            // parse input from WASD keys
            move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        // move player based on movement vector multiplied by speed
        playerRigidbody.velocity = move * speed;

        lookX = Mathf.Clamp(mouse.position.x - playerRigidbody.position.x, -1, 1);

        if (move.magnitude != 0)
        {
            if (move.x < 0)
            {   
                playerSprite.flipX = true;

                attackPos.localPosition = new Vector3(-0.5f, 0, 0);
            }
            else if(move.x > 0)
            {
                playerSprite.flipX = false;

                attackPos.localPosition = new Vector3(0.5f, 0, 0);
            }
        }
        else
        {
            if (lookX < 0)
            {
                playerSprite.flipX = true;

                attackPos.localPosition = new Vector3(-0.1f, 0, 0);
            }
            else if(lookX > 0)
            {
                playerSprite.flipX = false;

                attackPos.localPosition = new Vector3(0.1f, 0, 0);
            }
        }
    }

    void Update()
    {
        ManageAnimations();
     
        //Heal player when pressing 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Heal player if potions held are more than 0
            if(CoinCount.pot >= 1)
            {
                Heal();
            }

            //Healing function for potion
            void Heal()
            {
                currentHealth += 5;

                healthBar.SetHealth(currentHealth);
                CoinCount.pot -= 1;
                PlayerPrefs.SetInt("hMany", CoinCount.pot);
            }
        }

        if (currentHealth <= 0)
        {
            Dead();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (CoinCount.count <= 5000)
            {
                CoinCount.count += 100;

                PlayerPrefs.SetInt("amount", CoinCount.count);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if(CoinCount.count >= 100)
            {
                CoinCount.count -= 100;

                PlayerPrefs.SetInt("amount", CoinCount.count);
            }
        }
    }

    void ManageAnimations()
    {
        // change from idle animations to move animations based on isMoving boolean
        playerAnimator.SetBool("Moving", move.magnitude != 0);

        // call Attack1's function
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<PlayerAttack>().Attack();
            Debug.Log("Attacked!");
        }
        // play Attack2 animation on right click
        if (Input.GetMouseButtonDown(1))
        {
            GetComponent<ShootBow>().StartCoroutine("Shoot");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(3);
        }

        if (other.gameObject.CompareTag("Boss"))
        {
            TakeDamage(12);
        }

        if (other.gameObject.CompareTag("RedEnemy"))
        {
            TakeDamage(7);
        }


        if (other.gameObject.CompareTag("Heal"))
        {
            if (CoinCount.count >= 100)
            {
                CoinCount.pot += 1;

                PlayerPrefs.SetInt("hMany", CoinCount.pot);

                CoinCount.count -= 100;

                PlayerPrefs.SetInt("amount", CoinCount.count);
            }
        }

        void TakeDamage(int damage)
        {
            currentHealth -= damage;

            healthBar.SetHealth(currentHealth);
        }

        if (other.gameObject.CompareTag("Gold"))
        {
            ShowNumber(gd.ToString());

            CoinCount.count += 50;

            PlayerPrefs.SetInt("amount", CoinCount.count);

            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Bow"))
        {
            if (CoinCount.count >= 500)
            {
                //ShowNumber(CoinCount.count.ToString());

                ShootBow.available = true;

                bowImage.GetComponent<Image>().enabled = true;

                CoinCount.count -= 500;

                PlayerPrefs.SetInt("amount", CoinCount.count);

                Destroy(other.gameObject);
            }
        }
    }

    void Dead()
    {
        playerAnimator.Play("PlayerDie");

        playerRigidbody.velocity = Vector2.zero;

        this.enabled = false;
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