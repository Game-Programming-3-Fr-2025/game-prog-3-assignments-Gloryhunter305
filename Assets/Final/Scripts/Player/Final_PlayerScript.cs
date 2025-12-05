using UnityEngine;

public class Final_PlayerScript : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    [SerializeField] private Vector2 _movement;
    [SerializeField] private float _moveSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        moveCharacter(_movement);
    }

    void moveCharacter(Vector2 movement)
    {
        _rigidBody2D.MovePosition(_rigidBody2D.position + movement * _moveSpeed * Time.deltaTime);
    }
}
