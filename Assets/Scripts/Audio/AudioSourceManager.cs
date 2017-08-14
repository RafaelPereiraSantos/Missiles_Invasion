using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour {

    //Gerencia as fontes de som no gameObject

    float max;

    // Use this for initialization
    void Start()
    {
        _Update();

        this.max = this.GetComponent<AudioSource>().volume;

        this.GetComponent<AudioSource>().volume = 0;

    }

    void Update()
    {
        if (this.GetComponent<AudioSource>().volume < this.max)
        {
            this.GetComponent<AudioSource>().volume += 0.001f;
        }
    }

    public void _Update ()
    {
        

        if (GetComponent<AudioSource>() != null)
        {
            if (PlayerPrefs.GetInt("sound") == 1 && GameObject.Find("_GC").GetComponent<_GC>().isPaused == false)
                //caso o jogo não esteja pausado e o usuario definiu que quer ouvir som, ativa os sons, qualquer outro caso desabilita
            {
                _Play();
            }
            else
            {
                _Pause();
            }
        }
	}
	
	void _Pause()
    {        
        this.GetComponent<AudioSource>().mute = true;
    }

    void _Play()
    {
        this.GetComponent<AudioSource>().mute = false;
    }
}
