using UnityEngine;

/*
 * Sound class for use with AudioManager. In addition to storing an AudioClip to be played,
 * Sound stores other information about the sound that exists separately from the AudioSource's
 * values.
 */
[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0, 1)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

    [HideInInspector]
    public bool isPaused;
}
