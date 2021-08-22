using UnityEngine;

namespace Gameplay
{
    public class Player : Character
    {
        private Vector3 _directionMovement;

        private void Update()
        {
            Move();
        }

        protected override void Move()
        {
            CalculateDirectionMovement();
            TargetPosition = transform.position + _directionMovement * speed;

            base.Move();
        }

        private void CalculateDirectionMovement()
        {
            _directionMovement.x = Input.GetAxisRaw("Horizontal");
            _directionMovement.y = Input.GetAxisRaw("Vertical");
            _directionMovement *= Time.fixedDeltaTime;
        }
    }
}