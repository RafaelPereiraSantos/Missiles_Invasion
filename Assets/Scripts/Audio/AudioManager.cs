using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    //Esse controlador serve para gerenciar todos os efeitos sonoros que não se repetem, como uma explosao


    public Sound[] sound;

	// Use this for initialization
	void Awake ()
    {
		foreach(Sound s in sound)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;

        }
	}
	public void Play(string name)
        //inicia um audio com o nome fornecido
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            return;            
        }
        Sound s = Array.Find(sound, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    //inicia um audio com o nome fornecido
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            return;
        }
        Sound s = Array.Find(sound, sound => sound.name == name);
        s.source.Stop();
    }

    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        return s.source.isPlaying;
    }
}
