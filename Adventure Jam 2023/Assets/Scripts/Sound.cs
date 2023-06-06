using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0, 1)] public float volume = 1;
    [Range(0.1f, 3)] public float pitch = 1;

    [HideInInspector] public AudioSource source;
}
