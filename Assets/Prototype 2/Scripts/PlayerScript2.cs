using System.Collections.Generic;
using UnityEngine;

public class PlayerScript2 : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private Rigidbody2D _rigidBody2D;
    [SerializeField] private float _speed = 5f;

    private bool OnGround;
    private List<GameObject> touching;

    private void Start()
    {
        touching = new List<GameObject>();
        if (_rigidBody2D == null)
            _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Warp();
        PlayerMovement();
    }

    public void PlayerMovement()
    {

        float moveInput = Input.GetAxis("Horizontal"); // Gets input from A/D or Left/Right arrow keys
        // Use Rigidbody2D.velocity (not linearVelocity) so physics works correctly
        _rigidBody2D.linearVelocity = new Vector2(moveInput * _speed, _rigidBody2D.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            // Invert gravity by negating the gravityScale
            _rigidBody2D.gravityScale *= -1f;
            // Optionally zero vertical velocity so inversion is instantaneous
            _rigidBody2D.linearVelocity = new Vector2(_rigidBody2D.linearVelocity.x, 0f);
        }

    }

    public void Warp()
    {
        if (transform.position.y > 10f)
        {
            transform.position = new Vector3(transform.position.x, -6f, transform.position.z);
        }
        else if (transform.position.y < -6f)
        {
            transform.position = new Vector3(transform.position.x, 10f, transform.position.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Goal"))
        {
            //Teleport to start position
            transform.position = new Vector3(0, 0, 0);
        }
    }

    
    public bool CanJump()
    {
        return touching.Count > 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnGround = true;
        touching.Add(other.gameObject);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        OnGround = false;
        touching.Remove(other.gameObject);
    }
    

}
