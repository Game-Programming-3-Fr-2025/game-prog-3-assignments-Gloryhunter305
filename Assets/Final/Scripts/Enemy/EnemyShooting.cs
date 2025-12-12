using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    /*  Summary of this Script: 
     *  Acts as the range detector and shooter for enemy characters.
     *  When the player enters a specified range, the enemy shoots bullets at regular intervals.
    */
    [Header("References")]
    public GameObject Bullet;
    public Transform BulletSpawnPoint;

    private GameObject _player;

    [Header("Shooting Settings")]
    [SerializeField] private float shootingRange = 10f;
    [SerializeField] private float shootInterval = 2.0f;
    private float _currentShootInterval;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _currentShootInterval = shootInterval;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, _player.transform.position);
        Debug.Log("Distance to Player: " + distance);
        if (distance < shootingRange)
        {
            _currentShootInterval -= Time.deltaTime;

            if (_currentShootInterval <= 0f)
            {
                Shoot();
                _currentShootInterval = shootInterval; // Reset the interval
            }
        }

        
    }

    private void Shoot()
    {
        Instantiate(Bullet, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
    }
}
