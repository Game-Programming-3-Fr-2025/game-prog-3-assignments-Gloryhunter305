using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [Header("Projectile Settings")]
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _lifetime = 3f;

    private Vector2 _direction = Vector2.right;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        // schedule destruction
        Destroy(gameObject, _lifetime);
    }

    /// <summary>
    /// Launch the bullet in the given direction. Call immediately after Instantiate.
    /// </summary>
    public void Launch(Vector2 direction)
    {
        _direction = direction.normalized;

        if (_rigidbody2D == null)
            _rigidbody2D = GetComponent<Rigidbody2D>();

        if (_rigidbody2D != null)
        {
            _rigidbody2D.linearVelocity = _direction * _speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HealthManager enemyHealth = collision.gameObject.GetComponent<HealthManager>();
            
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1); // assuming bullet does 1 damage
            }
            else
            {
                Debug.LogWarning("Enemy does not have a HealthManager component.");
            }
            Destroy(gameObject);
        }
    }
}
