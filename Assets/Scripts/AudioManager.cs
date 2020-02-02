using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //store an array of sounds
    public Sound[] sounds;

    public static AudioManager instance;

    //this allows us to change the string in the inspector...
    public string mainTheme = "Main theme";

    //in order to play a specific song or SFX,
    //write "FindObjectOfType<AudioManager>().Play("nameOfSFX");

    void Awake()
    {
        //keeps it so that there's only one audio manager at a time?
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

        //automates the creation of audio sources object for us
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play(mainTheme);
    }

    public void Play(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }
}