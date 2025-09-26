using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript4 : MonoBehaviour
{
    //Components for Gameobject
    public Rigidbody2D RB;
    public float Speed = 5;
    public float JumpPower = 10;
    public float Gravity = 1;

    //GameObject's State
    public bool OnGround = false;
    public List<GameObject> touching;


    // Start is called before the first frame update
    void Start()
    {
        RB.gravityScale = Gravity;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vel = RB.linearVelocity;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            vel.x = -Speed;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            vel.x = Speed;
        }
        else
        {
            vel.x = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            vel.y = JumpPower;
        }

        RB.linearVelocity = vel;
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
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
