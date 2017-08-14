using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCrud : MonoBehaviour {

    public Text scoreValue_text;

    void OnEnable()
    {
        UpdateTimeText(GetHighScore());
    }

    public bool SetHighScore(int score = 0)
        //caso a pontuaçao fornecida for maior do que a grava, retorna true, ou seja, ha novo score
    {
        int value = 0;
        value = PlayerPrefs.GetInt("bestScore");

        if(score > value)
        {
            PlayerPrefs.SetInt("bestScore", score);
            return true;
        } else
        {
            return false;
        }
    }
	
	public int GetHighScore()
    {
        int value = 0;

        value = PlayerPrefs.GetInt("bestScore");
        
        return value;
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

        this.scoreValue_text.text = clockFormat;
        //atualiza o texto

    }
}
