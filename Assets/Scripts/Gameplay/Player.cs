using UnityEngine;

namespace Gameplay
{
    public class Player : Entity
    {
        public static Player Instance;
        public Weapon.Weapon weapon;
        public Camera mainCamera;

        public new Rigidbody2D rigidbody;
        private float _mouseAngle;

        public void Shoot()
        {
            if (Input.GetButtonDown("Fire1"))
                weapon.Shoot(_mouseAngle);
        }

        private void Awake()
        {
            Instance = this;
        }

        public void CalculateMouseAngle()
        {
            var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var lookDirection = mousePosition - transform.position;
            _mouseAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        }

        public override void Move()
        {
            var mH = Input.GetAxis ("Horizontal");
            var mV = Input.GetAxis ("Vertical");
            rigidbody.velocity = new Vector3 (mH * speed, mV * speed);
        }

   
    }
}