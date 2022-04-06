using System;
using Mechanics;
using Mechanics.MapGeneration;
using Ui;
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
            if (GameManager.IsGamePaused)
                return;
            
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

            player.health.Heal();
            // GameManager.Instance.CreateMap();
        }
    }
}