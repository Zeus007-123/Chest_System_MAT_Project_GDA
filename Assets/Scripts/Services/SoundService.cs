using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Services
{
    /*
        Enum for Sound Types.
    */
    public enum SoundType
    {
        BUTTON_CLICK,
        CHEST_UNLOCKED,
        BGM  // 🔹 Added Background Music SoundType
    }

    /// <summary>
    /// Serializable Class SoundInfo to maintain different sound properties.
    /// </summary>
    [System.Serializable]
    public class SoundInfo
    {
        public SoundType soundType;  // Sound type (Button Click, Chest Unlocked, etc.)
        public AudioClip clip;       // Audio file for this sound
        public bool loop;            // Should this sound loop?

        [HideInInspector]
        public AudioSource audioSource;  // AudioSource component to play this sound

        [Range(0, 1)]
        public float volume = 0.5f;  // Default volume level
    }

    /// <summary>
    /// SoundService MonoSingleton class. Handles all the Sounds in the Project.
    /// </summary>
    public class SoundService : GenericMonoSingleton<SoundService>
    {
        public SoundInfo[] Sounds; // List of sounds to play
        private AudioSource bgmAudioSource;  // 🔹 Separate AudioSource for Background Music

        private void Start()
        {
            PlayBGM();  // 🔹 Start playing background music when the game begins
        }

        protected override void Awake()
        {
            base.Awake();

            // 🔹 Initialize the BGM AudioSource
            bgmAudioSource = gameObject.AddComponent<AudioSource>();
            bgmAudioSource.loop = true;  // Ensures it keeps looping

            // 🔹 Initialize other sound effects
            for (int i = 0; i < Sounds.Length; i++)
            {
                Sounds[i].audioSource = gameObject.AddComponent<AudioSource>();
                Sounds[i].audioSource.loop = Sounds[i].loop;
                Sounds[i].audioSource.volume = Sounds[i].volume;
                Sounds[i].audioSource.clip = Sounds[i].clip;
            }
        }

        /*
            Plays Background Music (BGM) on loop.
        */
        public void PlayBGM()
        {
            SoundInfo bgmSound = Array.Find(Sounds, item => item.soundType == SoundType.BGM);
            if (bgmSound != null && bgmSound.clip != null)
            {
                if (!bgmAudioSource.isPlaying) // Prevent multiple BGM instances
                {
                    bgmAudioSource.clip = bgmSound.clip;
                    bgmAudioSource.volume = bgmSound.volume;
                    bgmAudioSource.Play();
                }
            }
            else
            {
                Debug.LogError("BGM SoundInfo is missing or clip is not assigned!");
            }
        }

        /*
            Stops Background Music (BGM).
        */
        public void StopBGM()
        {
            if (bgmAudioSource.isPlaying)
                bgmAudioSource.Stop();
        }

        /*
            Plays the Audio of specified SoundType.
        */
        public void PlayAudio(SoundType soundType)
        {
            SoundInfo soundInfo = Array.Find(Sounds, item => item.soundType == soundType);
            if (soundInfo != null && soundInfo.audioSource != null)
            {
                soundInfo.audioSource.Play();
            }
            else
            {
                Debug.LogError("Sound not found: " + soundType);
            }
        }

        /*
            Stops the Audio of specified SoundType.
        */
        public void StopAudio(SoundType soundType)
        {
            SoundInfo soundInfo = Array.Find(Sounds, item => item.soundType == soundType);
            if (soundInfo != null && soundInfo.audioSource != null)
            {
                soundInfo.audioSource.Stop();
            }
        }
    }
}
