using UnityEngine;

namespace Gameplay.Player
{
    public class Player : Character
    {
        private Vector3 _direction;

        private void Update()
        {
            _direction.x = Input.GetAxisRaw("Horizontal");
            _direction.y = Input.GetAxisRaw("Vertical");
            
            CalculateTargetPosition();
            Move();
        }

        protected override void CalculateTargetPosition()
        {
            _direction *= Time.fixedDeltaTime;
            TargetPosition = transform.position + _direction * speed;
        }

    }
}