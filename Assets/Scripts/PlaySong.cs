using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySong : MonoBehaviour {

    public AudioSource audio;

    public void _PlaySong()
    {
        audio.Play();
    }
}
