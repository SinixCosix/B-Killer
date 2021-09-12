using Gameplay.Enemy;
using UnityEngine;

namespace Gameplay.Weapon
{
    public class Bullet : Entity
    {
        public Rigidbody2D rb;

        private void FixedUpdate()
        {
            rb.velocity = transform.up * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.name);
            var enemy = other.gameObject.GetComponent<AbstractEnemy>();
            if (enemy == null) return;
            
            enemy.ApplyDamage(damage);
            Destroy(gameObject);
        }
    }
}