using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float upperBound = 4f;
    public float lowerBound = -4f;
    private Vector3 startPosition;
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
    }
}
