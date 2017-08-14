using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _GC : MonoBehaviour {

    public bool isPaused;
    public bool isOnPlaying = false;

    public Text timerValue_text;

    public float currentTime;

    void Update()
    {
        if (this.isOnPlaying)
        {
            if (this.isPaused)
            {
                return;
            }

            this.currentTime += Time.deltaTime;

            if (this.timerValue_text.IsActive())
            {
                //timerValue_text.text = string.Format("{0}", ((int)this.currentTime).ToString());
                UpdateTimeText(this.currentTime);
            }
        } else
        {
            this.currentTime = 0;
        }
    }

	public void _Pause()
    {
        this.isPaused = !this.isPaused;
        PauseEverthing();
    }

    public void _GameState()
    {
        this.isOnPlaying = !this.isOnPlaying;
    }

    public void _ResetTime(bool save = false)
    {
        this.currentTime = 0;
    }

    void PauseEverthing()
    {
        foreach(GameObject temp in GameObject.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if(temp.GetType() == typeof(ParticleSystem))
            {
               // temp.particleSystem.Play();
            }
        }
    }

    void UpdateTimeText(float newTime)
    {
        int allTime = (int)newTime;

        int minutes = allTime / 60;
        //define os minutos dividindo por 60 segundos

        int secounds = allTime - (minutes * 60);
        //define os segundos que restam

        string secoundStr = secounds.ToString();

        if (secounds < 10)
            //caso os segundos sejam menor que 10, adiciona um 0 a frente para ficar 01,02,03...
        {
            secoundStr = "0" + secounds.ToString();
        } 

        string clockFormat = string.Format("{0}:{1}", minutes, secoundStr);
        //formata o texto

        this.timerValue_text.text = clockFormat;
        //atualiza o texto

    }
}
