using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    //in the script you want, call this
    //AudioManagerNew.instance.Play("Sound Name");
    //then in the script array, make a sound with the "Sound Name" name and attach the sound effect

    public Sound[] sounds;
    public static AudioManager instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Sound s in sounds)
        {
            GameObject child = new(s.name);
            child.transform.SetParent(gameObject.transform, false);

            s.source = child.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
        {
            s.source.PlayOneShot(s.clip);
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }
    public void Play(string name, float pitchVariance)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
        {
            s.source.pitch = pitchVariance;
            //this is if u want the pitch to be random
            //s.source.pitch = s.pitch + UnityEngine.Random.Range(-pitchVariance, pitchVariance);
            s.source.PlayOneShot(s.clip);
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    void Update()
    {
        
    }
    public void IncreasePitch(Sound sound, float increase)
    {
        sound.pitch = sound.pitch + increase;
    }
}
