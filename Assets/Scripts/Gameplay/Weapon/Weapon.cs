using UnityEngine;

namespace Gameplay.Weapon
{
    public class Weapon : MonoBehaviour
    {
        public Transform firePoint;
        public GameObject bulletPrefab;
        
        public void Shoot(float rotation)
        {
            firePoint.eulerAngles = new Vector3(0,0, rotation);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}