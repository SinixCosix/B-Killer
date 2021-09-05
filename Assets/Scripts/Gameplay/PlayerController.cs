using UnityEngine;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        public Player player;

        private void Start()
        {
            player.health.Death += PlayerDeath;
        }

        private void Update()
        {
            player.Move();
        }

        private void PlayerDeath()
        {
            Debug.Log("Player death event");
            Respawn();
        }

        private void Respawn()
        {
            player.transform.position = Vector3.zero;
            player.health.Heal();
        }
    }
}