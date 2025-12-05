using UnityEngine;
public class EnemyScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float upperBound = 4f;
    [SerializeField] private float lowerBound = -4f;
    private Vector3 startPosition;

    [Header("Enemy Components")]
    private int _health = 3;
    private bool isActive = true;
    private int moveDirection = 1; // 1 = up, -1 = down

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (isActive)
        {
            // Move up and down
            transform.Translate(Vector2.up * moveSpeed * moveDirection * Time.deltaTime);

            // Reverse direction at bounds
            if (transform.position.y >= upperBound)
            {
                moveDirection = -1;
            }
            else if (transform.position.y <= lowerBound)
            {
                moveDirection = 1;
            }
        }
        else
        {
            // Reset position and reactivate
            transform.position = startPosition;
            _health = 3;
            isActive = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            if (_health > 0)
            {
                _health--;
            }
            else
            {
                isActive = false;
            }
        }
    }
}
