using System;
using UnityEngine;

namespace Mechanics
{
    public class Health : MonoBehaviour
    {
        public float maxHp = 5;

        public delegate void MethodContainer();
        public event MethodContainer Death;
        
        private float _currentHp;

        private void Awake() => _currentHp = maxHp;

        public void ApplyDamage(int damage)
        {
            _currentHp -= damage;
            Debug.Log($"Current hp: {_currentHp}");
            if (IsDead())
                Death?.Invoke();
        }

        private bool IsDead() => _currentHp < 0.01;

        public void Heal()
            => _currentHp = maxHp;
    }
}