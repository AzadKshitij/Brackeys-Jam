using System;
using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        { 
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            
        }
    }

    public void playSound(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.Play();
    }



    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0,1)]
        public float Volume;
        [Range(0,1)]
        public float pitch;

        [HideInInspector]
        public AudioSource source;
    }



}
