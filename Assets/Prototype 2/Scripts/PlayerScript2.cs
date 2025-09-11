using UnityEngine;

public class PlayerScript2 : MonoBehaviour
{
    public Rigidbody2D rb;
    public float Speed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Warp();
        PlayerMovement();
    }

    public void PlayerMovement()
    {
        Vector2 vel = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            vel.x = Speed;
        }
        //If I hold the left arrow, the player should move left. . .
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            vel.x = -Speed;
        }
        //If I hold the up arrow, the player should move up. . .
        if (Input.GetKey(KeyCode.UpArrow))
        {
            vel.y = Speed;
        }
        //If I hold the down arrow, the player should move down. . .
        if (Input.GetKey(KeyCode.DownArrow))
        {
            vel.y = -Speed;
        }

        rb.linearVelocity = vel;

    }

    public void Warp()
    {
        // if (transform.position.x > 9.5f)
        // {
        //     transform.position = new Vector3(-9.5f, transform.position.y, transform.position.z);
        // }
        // if (transform.position.x < -5.3f)
        // {
        //     transform.position = new Vector3(5.3f, transform.position.y, transform.position.z);
        // }
        // if (transform.position.y > 5.5f)
        // {
        //     transform.position = new Vector3(transform.position.x, -5.5f, transform.position.z);
        // }
        if (transform.position.y < -5.3f)
        {
            transform.position = new Vector3(transform.position.x, 5.3f, transform.position.z);
        }
    }
    

}
