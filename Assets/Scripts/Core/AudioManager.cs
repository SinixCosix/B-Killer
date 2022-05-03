using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Core
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        public Sound[] sounds;
        // Start is called before the first frame update
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            
            foreach (var sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                var source = sound.source;
                source.clip = sound.clip;

                source.volume = sound.volume;
                source.pitch = sound.pitch;
                source.loop = sound.loop;
                source.reverbZoneMix = 0f;
            }
        }

        public void Play(string soundName)
        {
            var sound = FindSound(soundName);
            if (sound == null)
                return;
            
            if (!sound.source.isPlaying)
                sound.source.Play();
        }
        
        public void Pause(string soundName)
        {
            var sound = FindSound(soundName);
            sound?.source.Pause();
        }

        private Sound FindSound(string name)
        {
            var sound = Array.Find(sounds, item => item.name == name);
            if (sound == null)
                Debug.Log($"Sound {name} wasn't found");

            return sound;
        }
    }
}

