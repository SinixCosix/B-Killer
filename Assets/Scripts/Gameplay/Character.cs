using UnityEngine;

namespace Gameplay
{
    public abstract class Character : MonoBehaviour
    {
        public float speed = 10f;
        
        protected Vector2 TargetPosition;
        protected abstract void CalculateTargetPosition();
        
        protected void Move()
        {
            var position = transform.position;
            position = Vector2.MoveTowards(position, TargetPosition, speed * Time.deltaTime);
            transform.position = position;
        }
    }
}