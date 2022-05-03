using System;
using Core;
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
            var direction = new Vector2(Input.GetAxis("Horizontal"),
                                        Input.GetAxis("Vertical"));
            player.rigidbody.velocity = direction * player.speed;
            
            // if (player.rigidbody.velocity != Vector2.zero)
            // AudioManager.Instance.Play("Player Footsteps");
            // else
            // AudioManager.Instance.Pause("Player Footsteps");
        }

        private void OnPlayerDeath()
        {
            Debug.Log("Player death event");

            player.health.Heal();
            // GameManager.Instance.CreateMap();
        }
    }
}