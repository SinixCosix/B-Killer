using System;
using UnityEngine;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        public Player player;

        private void Start()
        {
            player.Death += PlayerDeath;
        }

        private void Update()
        {
            player.Move();
        }

        private void PlayerDeath()
        {
            Debug.Log("Player death event");
            player.transform.position = Vector3.zero;
        }
    }
}