using System;
using UnityEngine;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        public Player player;
        [NonSerialized] public Vector2 SpawnPoint;

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
            player.transform.position = SpawnPoint;
            player.health.Heal();
        }
    }
}