using System;
using Mechanics;
using Mechanics.MapGeneration;
using UnityEngine;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        public Player player;

        private void Start()
        {
            player = Player.Instance;
            player.health.Death += PlayerDeath;
        }

        private void Update()
        {
            player.Shoot();
            player.CalculateMouseAngle();
            player.Move();
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