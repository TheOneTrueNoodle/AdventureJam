using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup music;
    public AudioMixerGroup SFX;

    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            GameObject soundObj = new GameObject(s.name);
            s.source = soundObj.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
