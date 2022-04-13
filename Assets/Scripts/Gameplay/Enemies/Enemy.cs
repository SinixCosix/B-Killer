using Pathfinding;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class Enemy : Entity
    {
        private Player _player;

        protected void Awake()
        {
            _player = Player.Instance;
            health.Death += Death;
        }

        private void Death()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            var destinationSetter = gameObject.GetComponent<AIDestinationSetter>();
            destinationSetter.target = _player.transform;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var playerComponent = collision.gameObject.GetComponent<Player>();
            if (playerComponent != null)
            {
                _player.ApplyDamage(damage);
            }
        }
    }
}