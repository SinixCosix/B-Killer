using System;
using Gameplay.Enemy;
using Mechanics.Rooms;
using UnityEngine;

namespace Gameplay.Weapon
{
    public class Bullet : Entity
    {
        private void Update()
        {
            transform.Rotate(0,0,750 * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemy = other.gameObject.GetComponent<AbstractEnemy>();
            if (enemy != null) enemy.ApplyDamage(damage);

            var player = other.gameObject.GetComponent<Player>();
            if (player != null) return;

            var room = other.gameObject.GetComponent<Room>();
            if (room != null) return;

            var bullet = other.gameObject.GetComponent<Bullet>();
            if (bullet != null) return;

            Destroy(gameObject);
        }
    }
}