using System;
using Mechanics;

using UnityEngine;

namespace Gameplay.Enemy
{
    public abstract class AbstractEnemy : Entity
    {
        protected Player Player;
        public float minPlayerDistance = 5f;
        public float minTargetDistance = 0.01f;

        protected void Awake()
        {
            Player = Player.Instance;
            health.Death += Death;
            
        }

        private void Death()
        {
            Destroy(gameObject);
        }

        protected bool IsNextToPlayer()
        {
            var playerPosition = Player.transform.position;
            var currentPosition = transform.position;

            return Vector2.Distance(currentPosition, playerPosition) < minPlayerDistance;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var playerComponent = collision.gameObject.GetComponent<Player>();
            if (playerComponent != null)
            {
                Player.ApplyDamage(damage);
            }
        }
    }
}