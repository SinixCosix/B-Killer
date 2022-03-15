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

        private void Start()
        {
            player = Player.Instance;
            player.health.Death += PlayerDeath;
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
            Debug.Log(player.MouseAngle);
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

        private void PlayerDeath()
        {
            Debug.Log("Player death event");
            Respawn();
        }

        public void Respawn()
        {
            var room = GameManager.Instance.Rooms[0];
            player.transform.position = room.transform.position;
            player.health.Heal();
        }
    }
}