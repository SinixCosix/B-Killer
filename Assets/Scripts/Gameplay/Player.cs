using Gameplay.Enemy;
using Mechanics;
using UnityEngine;

namespace Gameplay
{
    public class Player : Character
    {
        public Health health;
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
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var col = collision.gameObject.GetComponent<AbstractEnemy>();
            if (col != null)
            {
                health.Damage();
            }
        }
    }
}