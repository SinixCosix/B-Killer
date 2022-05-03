using System;
using UnityEngine;

namespace Core
{
    public class SoundEffect : MonoBehaviour
    {
        public AudioSource source;

        private void Awake()
        {
            source.Play();
            Destroy(gameObject, source.clip.length);
        }
    }
}