using System.Collections.Generic;
using UnityEngine;

public class PlayerScript2 : MonoBehaviour
{
    public Rigidbody2D rb;
    public float Speed = 5f;

    public bool OnGround = false;
    public bool FacingLeft = false;
    public List<GameObject> touching;

    // Update is called once per frame
    void Update()
    {
        Warp();
        PlayerMovement();
    }

    public void PlayerMovement()
    {

        float moveInput = Input.GetAxis("Horizontal"); // Gets input from A/D or Left/Right arrow keys
        rb.linearVelocity = new Vector2(moveInput * Speed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            rb.gravityScale = -rb.gravityScale; // Inverts gravity when spacebar is pressed
        }

    }

    public void Warp()
    {
        if (transform.position.y > 5.3f)
        {
            transform.position = new Vector3(transform.position.x, -5.3f, transform.position.z);
        }
        else if (transform.position.y < -5.3f)
        {
            transform.position = new Vector3(transform.position.x, 5.3f, transform.position.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
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
