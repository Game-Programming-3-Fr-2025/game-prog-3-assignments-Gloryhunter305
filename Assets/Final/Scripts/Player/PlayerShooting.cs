using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate = 0.5f;
    private float _nextFireTime = 0f;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= _nextFireTime)
        {
            if (_firePoint == null)
            {
                Debug.LogWarning("FirePoint not assigned on PlayerShooting.");
                return;
            }

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 aimDir = (mousePos - _firePoint.position);
            if (aimDir.sqrMagnitude < 0.0001f)
                aimDir = Vector2.right;

            Vector2 aimDirNorm = aimDir.normalized;
            float angle = Mathf.Atan2(aimDirNorm.y, aimDirNorm.x) * Mathf.Rad2Deg;
            ShootTriple(aimDirNorm, angle);
            _nextFireTime = Time.time + _fireRate;
        }
    }

    [SerializeField] private float _spreadAngle = 10f; // degrees between center and side bullets

    private void ShootTriple(Vector2 aimDirection, float baseAngle)
    {
        if (_bulletPrefab == null)
        {
            Debug.LogWarning("Bullet prefab not assigned on PlayerShooting.");
            return;
        }

        float[] angles = { baseAngle, baseAngle + _spreadAngle, baseAngle - _spreadAngle };

        foreach (float a in angles)
        {
            float rad = a * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            GameObject go = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.Euler(0, 0, a));
            PlayerBulletScript proj = go.GetComponent<PlayerBulletScript>();
            if (proj != null)
            {
                proj.Launch(dir);
            }
        }
    }
}
