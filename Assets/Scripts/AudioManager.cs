using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    //in the script you want, call this
    //AudioManager.instance.Play("Sound Name");
    //then in the script array, make a sound with the "Sound Name" name and attach the sound effect

    public Sound[] sounds;
    // Array of sounds for music tracks.
    public Sound[] musicTracks;
    public Sound currentMusicTrack;

    public static AudioManager instance;

    private void Awake()
    {
        // Singleton code.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
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
            foreach (Sound s in musicTracks)
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
    public void PlayPitch(string name, float pitchVariance)
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

    public void IncreasePitch(Sound sound, float increase)
    {
        sound.pitch = sound.pitch + increase;
    }
    /*public void PlayMusic(string name)
    {
        PlayMusic(name, true, true);
    }*/

    // Plays the music track with the given name.
    // If stopPrevious is true, the previous music track will be stopped. Otherwise, it will be paused.
    // If startFromBeginning is true, the current music track will be started from the beginning.
    // Otherwise, it will be unpaused.
    public void PlayMusic(string name, bool stopPrevious, bool startFromBeginning)
    {
        Debug.Log("played music");
        Sound s = Array.Find(musicTracks, sound => sound.name == name);

        // Only start playing the music track if it's non-null and not the music
        // track that's currently playing.
        if (s != null && s != currentMusicTrack)
        {
            if (currentMusicTrack != null && currentMusicTrack.source != null)
            {
                if (stopPrevious)
                {
                    currentMusicTrack.source.Stop();
                }
                else
                {
                    currentMusicTrack.source.Pause();
                    s.isPaused = true;
                }
            }

            currentMusicTrack = s;

            if (startFromBeginning || !s.isPaused)
            {
                Debug.Log("Playing audio: " + s.name);
                s.source.Play();
            }
            else
            {
                s.source.UnPause();
                s.isPaused = false;
            }
        }
        else if (s == null)
        {
            Debug.LogWarning("Music track: " + name + " not found!");
        }
    }
}
