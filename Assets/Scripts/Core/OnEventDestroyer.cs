using UnityEngine;

namespace Core
{
    public class OnEventDestroyer : MonoBehaviour
    {
        public void OnEvent()
        {
            Destroy(gameObject);
        }
    }
}