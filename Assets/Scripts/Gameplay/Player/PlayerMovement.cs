using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 10f;
        public Rigidbody2D rigidbody;


        private Vector2 _movement;

        private void Update()
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate()
        {
            _movement *= Time.fixedDeltaTime;
            Move();
        }

        private void Move()
        {
            var newPosition = rigidbody.position + _movement * speed;
            rigidbody.MovePosition(newPosition);
        }
    }
}