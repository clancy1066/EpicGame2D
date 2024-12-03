using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

// All Audio Enums
public enum AUDIO_LOOKUP
{
    CHEST_OPEN
}

[System.Serializable]
public class AudioEntry
{
    public AUDIO_LOOKUP key; // Enum key for the dictionary
    public AudioClip clip;   // Audio clip associated with the key
}

public class JukeBox : MonoBehaviour
{
    private static AudioSource audioSource;

    [SerializeField]
    private List<AudioEntry> audioEntryList = new List<AudioEntry>(); // Editable in Inspector

    private static Dictionary<AUDIO_LOOKUP, AudioClip> audioEntryMap = new Dictionary<AUDIO_LOOKUP, AudioClip>(); // Runtime dictionary

    void Awake()
    {
        // Add or get the AudioSource component
        audioSource = gameObject.GetComponent<AudioSource>();
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Populate the dictionary from the list 
        foreach (var entry in audioEntryList)
        {
            if (!audioEntryMap.ContainsKey(entry.key))
            {
                audioEntryMap.Add(entry.key, entry.clip);
            }
        }
    }

    // Example: Get AudioClip by AUDIO_LOOKUP key
    public AudioClip GetClip(AUDIO_LOOKUP lookup)
    {
        return audioEntryMap.TryGetValue(lookup, out var clip) ? clip : null;
    }

    static public bool PlayClip(AUDIO_LOOKUP lookup)
    {
        AudioClip theClip = audioEntryMap.TryGetValue(lookup, out var clip) ? clip : null;

        if (theClip!=null)
        {
            audioSource.clip = theClip;
            audioSource.Play();
            return true;
        }

        return false;
    }
}
