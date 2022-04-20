using Ui;
using UnityEngine;

namespace Gameplay
{
    public class Player : Entity
    {
        public static Player Instance;
        public Weapon.Weapon weapon;
        public Camera mainCamera;
        public HealthBar healthBar;

        public new Rigidbody2D rigidbody;

        public float MouseAngle { get; private set; }
        
        private void Awake()
        {
            Instance = this;
            healthBar.SetHealthMax((int)health.maxHp);
        }

        public void Shoot()
        {
            weapon.Shoot(MouseAngle);
        }

        public void CalculateMouseAngle()
        {
            var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var lookDirection = mousePosition - transform.position;
            MouseAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        }

        public override void ApplyDamage(int value)
        {
            base.ApplyDamage(value);
            healthBar.SetHealth((int)health.CurrentHp);
        }
    }
}