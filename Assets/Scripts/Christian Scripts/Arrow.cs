using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void FixedUpdate()
    {
        Vector2 dir = GetComponent<Rigidbody2D>().velocity;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {   
            Destroy(gameObject);
        }
    }
}