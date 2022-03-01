using Mechanics.MapGeneration;
using UnityEngine;

namespace Mechanics
{
    public class RoomBorder : MonoBehaviour
    {
       
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Some Collider" + collision);
        }
    }
}