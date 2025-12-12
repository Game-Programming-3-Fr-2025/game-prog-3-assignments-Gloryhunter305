using System.Collections;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody2D _rigidbody2D;

    [Header("Projectile Settings")]
    [SerializeField] private float force = 10f; // used as speed
    [SerializeField] private float slowedForce = 3f;

    private bool isSlowed = false;
    [SerializeField] private float _lifetime = 5f;

    private Vector2 _direction = Vector2.right;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_player != null)
        {
            _direction = (_player.transform.position - transform.position).normalized;
        }

        if (_rigidbody2D != null)
        {
            // set initial velocity instead of applying continuous forces
            _rigidbody2D.linearVelocity = _direction * force;
        }

        StartCoroutine(DestroyAfterTime());
    }

    private void FixedUpdate()
    {
        if (_rigidbody2D == null)
            return;

        float currentSpeed = isSlowed ? slowedForce : force;
        _rigidbody2D.linearVelocity = _direction * currentSpeed;
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(_lifetime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SlowZone"))
        {
            isSlowed = true;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            // attempt to apply damage if receiver implements it, else send message
            other.gameObject.SendMessage("TakeDamage", 20, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SlowZone"))
        {
            isSlowed = false;
        }
    }
}
