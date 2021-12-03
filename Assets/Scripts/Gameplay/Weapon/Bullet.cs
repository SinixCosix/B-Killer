﻿using Gameplay.Enemy;
using UnityEngine;

namespace Gameplay.Weapon
{
    public class Bullet : Entity
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemy = other.gameObject.GetComponent<AbstractEnemy>();
            if (enemy != null)
                enemy.ApplyDamage(damage);

            var player = other.gameObject.GetComponent<Player>();
            if (player != null)
                return;

            Destroy(gameObject);
        }
    }
}