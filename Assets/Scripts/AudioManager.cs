using System;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// This class is responsible for sound management, it can play or stop a specific audio file
/// </summary>
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; //List of stored audio files
    
    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.spatialBlend = sound.spatialBlend;
            sound.audioSource.loop = sound.loop;
        }        
    }

    /// <summary>
    /// Searches for a specified audio file with a given name, and plays that file if found. if not found prints
    /// a message in the debugger.
    /// </summary>
    /// <param name="name">The name of the audio file to search for and play</param>
    public void play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.audioSource.Play();   
        }
        else
        {
            Debug.Log("Could not find sound with name: " + name);
        }
    }
    
    /// <summary>
    /// Searches for a specified audio file with a given name, and stops that file if found and playing.
    /// if not found prints a message in the debugger.
    /// </summary>
    /// <param name="name">The name of the audio file to search for and stop</param>
    public void stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.audioSource.Stop();   
        }
        else
        {
            Debug.Log("Could not find sound with name: " + name);
        }
    }
    
    /// <summary>
    /// Searches for a specified audio file with a given name, and plays that file once til the end if found.
    /// if not found prints a message in the debugger.
    /// </summary>
    /// <param name="name">The name of the audio file to search for and play once</param>
    public void playOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.audioSource.PlayOneShot(s.clip);   
        }
        else
        {
            Debug.Log("Could not find sound with name: " + name);
        }
    }
}
