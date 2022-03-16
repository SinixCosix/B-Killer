using System;
using Mechanics;
using Mechanics.MapGeneration;
using UnityEngine;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        public Player player;
        public Animator animator;
        
        private static readonly int AnimatorMouseAngle = Animator.StringToHash("MouseAngle");

        private void Awake()
        {
            player = Player.Instance;
            player.health.Death += OnPlayerDeath;
        }

        private void Update()
        {
            player.CalculateMouseAngle();
            SetAnimatorProperties();
            Shoot();
            Move();
        }

        private void SetAnimatorProperties()
        {
            animator.SetFloat(AnimatorMouseAngle, (player.MouseAngle));
        }
        private void Shoot()
        {
            if (Input.GetButtonDown("Fire1"))
                player.Shoot();
        }
        private void Move()
        {
            var mH = Input.GetAxis("Horizontal");
            var mV = Input.GetAxis("Vertical");
            player.rigidbody.velocity = new Vector3(mH * player.speed,
                mV * player.speed);
        }

        private void OnPlayerDeath()
        {
            Debug.Log("Player death event");
            
            GameManager.Instance.CreateMap();
            player.health.Heal();
        }
    }
}