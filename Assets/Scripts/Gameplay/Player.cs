using UnityEngine;

namespace Gameplay
{
    public class Player : Entity
    {
        public static Player Instance;
        public Weapon.Weapon weapon;
        public Camera mainCamera;

        public new Rigidbody2D rigidbody;

        public float MouseAngle { get; private set; }

        public void Shoot()
        {
            weapon.Shoot(MouseAngle);
        }

        private void Awake()
        {
            Instance = this;
        }

        public void CalculateMouseAngle()
        {
            var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var lookDirection = mousePosition - transform.position;
            MouseAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        }
    }
}