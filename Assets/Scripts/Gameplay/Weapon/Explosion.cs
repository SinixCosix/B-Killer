using UnityEngine;

namespace Gameplay.Weapon
{
    public class Explosion : MonoBehaviour
    {
        public AudioSource source;
        
        public void OnEvent()
        {
            Destroy(gameObject, source.clip.length);
            source.Play();
            // gameObject.SetActive(false);
        }
    }
}