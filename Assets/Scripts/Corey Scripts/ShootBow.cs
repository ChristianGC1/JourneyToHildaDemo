using System.Collections;
using UnityEngine;

public class ShootBow : MonoBehaviour
{
    public static bool available;

    private Transform firePoint;

    public GameObject arrowPrefab;

    public float arrowSpeed = 2f;

    public static float bowCooldown = 1f;

    private bool bowReady = true;

    private void Awake()
    {
        firePoint = GameObject.Find("AttackPos").transform;
    }

    public IEnumerator Shoot()
    {
        if (bowReady && available)
        {
            bowReady = false;

            GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);

            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

            Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 aimDirection = target - rb.position;

            rb.velocity = aimDirection.normalized * arrowSpeed;

            yield return new WaitForSeconds(bowCooldown);

            bowReady = true;
        }
    }
}