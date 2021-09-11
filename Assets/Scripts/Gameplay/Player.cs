using UnityEngine;

namespace Gameplay
{
    public class Player : Entity
    {
        private Vector3 _directionMovement;

        public override void Move()
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