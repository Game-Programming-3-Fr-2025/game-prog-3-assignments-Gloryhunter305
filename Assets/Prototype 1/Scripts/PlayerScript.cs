using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [Header("Rigidbody2D Component")]
    [SerializeField] private Rigidbody2D rb;
    public float Speed = 5f;

    public ProjectileScript projectile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (SceneManager.GetActiveScene().name == "PrototypeOneMain")
        {
            PlayerShoot();
        }
    }


    public void Movement()
    {
        Vector2 vel = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.D))
        {
            vel.x = Speed;
        }
        //If I hold the left arrow, the player should move left. . .
        if (Input.GetKey(KeyCode.A))
        {
            vel.x = -Speed;
        }
        //If I hold the up arrow, the player should move up. . .
        if (Input.GetKey(KeyCode.W))
        {
            vel.y = Speed;
        }
        //If I hold the down arrow, the player should move down. . .
        if (Input.GetKey(KeyCode.S))
        {
            vel.y = -Speed;
        }

        rb.linearVelocity = vel;

    }

    public void PlayerShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            ProjectileScript newProjectile = Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, angle));
            newProjectile.rb.linearVelocity = direction * newProjectile.speed;

        }
    }
}
