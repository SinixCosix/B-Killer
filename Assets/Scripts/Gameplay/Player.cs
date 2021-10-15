using UnityEngine;

namespace Gameplay
{
    public class Player : Entity
    {
        public Vector3 movementDirection;
        public Weapon.Weapon weapon;
        public Camera mainCamera;

        private float _mouseAngle;

        public void Shoot()
        {
            if (Input.GetButtonDown("Fire1"))
                weapon.Shoot(_mouseAngle);
        }

        public void CalculateMouseAngle()
        {
            var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var lookDirection = mousePosition - transform.position;
            _mouseAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        }

        public override void Move()
        {
            CalculateDirectionMovement();
            TargetPosition = transform.position + movementDirection * speed;

            base.Move();
        }

        private void CalculateDirectionMovement()
        {
            movementDirection.x = Input.GetAxisRaw("Horizontal");
            movementDirection.y = Input.GetAxisRaw("Vertical");
            movementDirection *= Time.fixedDeltaTime;
        }
    }
}