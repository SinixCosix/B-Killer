using UnityEngine;

namespace Gameplay.Weapon
{
    public class Weapon : MonoBehaviour
    {
        public Transform firePoint;
        public GameObject bulletPrefab;

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
                Shoot();
        }

        private void Shoot()
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            
        }
    }
}