using System.Collections;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float speed = 10f;
    public float slowedSpeed = 3f; // Add this for slowed speed
    public float lifetime = 5f;
    public int damage = 1;
    public Rigidbody2D rb;

    private bool isSlowed = false; // Track if projectile is slowed

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = isSlowed ? slowedSpeed : speed;
        rb.linearVelocity = transform.right * currentSpeed;
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Example: Slow down when entering a collider with tag "SlowZone"
        if (other.gameObject.CompareTag("SlowZone"))
        {
            isSlowed = true;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Restore speed when leaving the slow zone
        if (other.gameObject.CompareTag("SlowZone"))
        {
            isSlowed = false;
        }
    }
}
