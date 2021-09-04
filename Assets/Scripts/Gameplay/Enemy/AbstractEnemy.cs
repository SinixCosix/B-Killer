using System;
using UnityEngine;

namespace Gameplay.Enemy
{
    public abstract class AbstractEnemy : Character
    {
        public Player player;
        public float minPlayerDistance = 5f;
        public float minTargetDistance = 0.01f;

        protected bool IsNextToPlayer()
        {
            var playerPosition = player.transform.position;
            var currentPosition = transform.position;

            return Vector2.Distance(currentPosition, playerPosition) < minPlayerDistance;
        }

        

        private void OnTriggerEnter(Collider other)
        {
            var col = other.gameObject.GetComponent<Player>();
            Debug.Log("on trigger");
            if (col != null)
            {
                Debug.Log("Triggered!");
            }
        }
    }
}