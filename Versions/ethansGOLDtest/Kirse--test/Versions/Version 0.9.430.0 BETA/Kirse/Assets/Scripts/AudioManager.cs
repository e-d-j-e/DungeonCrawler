using UnityEngine.Audio;
using UnityEngine;
using System;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager inst;

    // Start is called before the first frame update
    void Awake()
    {
        if(inst==null)
        {
            inst = this;
            Debug.Log("NULL Instance");
        }
        else
        {
            Debug.Log("Should Delete");
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

    }

    public void Play(string name)
    {
        Sound s= Array.Find(sounds, sound => sound.name==name);
        if (s == null)
        {
            Debug.LogWarning("ERROR-" + name + "not found");
            return;
        }
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
