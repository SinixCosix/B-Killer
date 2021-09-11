using System;
using UnityEngine;

namespace Gameplay.Enemy
{
    public abstract class AbstractEnemy : Entity
    {
        public Player player;
        public float minPlayerDistance = 5f;
        public float minTargetDistance = 0.01f;

        protected void Awake()
        {
            health.Death += Death;

        }

        private void Death()
        {
            Destroy(gameObject);
        }

        protected bool IsNextToPlayer()
        {
            var playerPosition = player.transform.position;
            var currentPosition = transform.position;

            return Vector2.Distance(currentPosition, playerPosition) < minPlayerDistance;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var playerComponent = collision.gameObject.GetComponent<Player>();
            if (playerComponent != null)
            {
                player.ApplyDamage(damage);
            }
        }
    }
}