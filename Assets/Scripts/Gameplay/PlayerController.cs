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
        
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");

        private void Start()
        {
            player = Player.Instance;
            player.health.Death += PlayerDeath;
        }

        private void Update()
        {
            player.Shoot();
            player.CalculateMouseAngle();
            Move();
        }

        private void Move()
        {
            var mH = Input.GetAxis("Horizontal");
            var mV = Input.GetAxis("Vertical");
            player.rigidbody.velocity = new Vector3(mH * player.speed,
                mV * player.speed);
            animator.SetInteger(Horizontal, (int)mH);
            animator.SetInteger(Vertical, (int)mV);
            
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