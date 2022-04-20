using System;
using Mechanics;
using UnityEngine;

namespace Gameplay
{
    public abstract class Entity : MonoBehaviour
    {
        public float speed = 5f;
        public Health health;
        public int damage = 1;
        
        protected Vector2 TargetPosition;
        
        public virtual void Move()
        {
            var position = transform.position;
            position = Vector2.MoveTowards(position, TargetPosition, speed * Time.deltaTime);
            transform.position = position; 
        }

        // TODO: move to PlayerController
        public virtual void ApplyDamage(int value)
        {
            health.ApplyDamage(value);
        }
        
    }
}