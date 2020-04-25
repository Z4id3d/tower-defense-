using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Represents a sound and holds some attributes related to that audio clip
/// </summary>
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    
    [Range(0f,1f)]
    public float volume;
    
    [Range(-3f,3f)]
    public float pitch;
    
    [Range(0f,1f)]
    public float spatialBlend;

    public bool loop;
    
    [HideInInspector]
    public AudioSource audioSource;

}
