using Gameplay;
using Mechanics.MapGeneration;
using UnityEngine;

namespace Mechanics
{
    public class RoomBorder : MonoBehaviour
    {
        public delegate void MethodContainer();

        public event MethodContainer PlayerTriggered;

        private void OnTriggerEnter2D(Collider2D col)
        {
            var player = col.gameObject.GetComponent<Player>();
            if (!player) return;

            Debug.Log("Player has entered in room");
            PlayerTriggered?.Invoke();
        }
    }
}