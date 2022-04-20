using System;
using UnityEngine;

namespace Mechanics
{
    public class Health : MonoBehaviour
    {
        public float maxHp = 5;

        public delegate void MethodContainer();
        public event MethodContainer Death;

        public float CurrentHp { get; private set; }

        private void Awake() => CurrentHp = maxHp;

        public void ApplyDamage(int damage)
        {
            CurrentHp -= damage;
            if (IsDead())
                Death?.Invoke();
        }

        private bool IsDead() => CurrentHp < 0.01;

        public void Heal()
            => CurrentHp = maxHp;
    }
}