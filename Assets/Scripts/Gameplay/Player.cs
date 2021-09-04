using Gameplay.Enemy;
using UnityEngine;

namespace Gameplay
{
    public class Player : Character
    {
        public delegate void MethodContainer();
        public event MethodContainer Death;
        
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
                // emit enemyCollisionEvent
                Death?.Invoke();
            }
        }
    }
}