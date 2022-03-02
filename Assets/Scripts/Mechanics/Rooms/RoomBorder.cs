using Gameplay;
using UnityEngine;

namespace Mechanics.Rooms
{
    public class RoomBorder : MonoBehaviour
    {
        public delegate void MethodContainer();

        public event MethodContainer PlayerTriggered;

        private void OnTriggerEnter2D(Collider2D col)
        {
            var player = col.gameObject.GetComponent<Player>();
            if (!player) return;

            PlayerTriggered?.Invoke();
        }
    }
}