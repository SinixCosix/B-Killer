using UnityEngine;

namespace Gameplay
{
    public abstract class Character : MonoBehaviour
    {
        public float speed = 5f;
        
        protected Vector2 TargetPosition;
        
        public virtual void Move()
        {
            var position = transform.position;
            position = Vector2.MoveTowards(position, TargetPosition, speed * Time.deltaTime);
            transform.position = position;
        }
    }
}