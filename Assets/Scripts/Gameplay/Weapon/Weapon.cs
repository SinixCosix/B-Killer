using UnityEngine;

namespace Gameplay.Weapon
{
    public class Weapon : MonoBehaviour
    {
        public Transform firePoint;
        public GameObject bulletPrefab;

        public float bulletForce = 20f;

        public void Shoot(float rotation)
        {
            firePoint.eulerAngles = new Vector3(0, 0, rotation);
            var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            var rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
    }
}