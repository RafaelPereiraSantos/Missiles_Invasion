using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour {

    public enum colors
    {
        blue,green,yellow,red
    }

    public colors color;

    [Header("valor da pontuaçao")]
    public Text BestScoreValue_text;
    public Text currentScore_text;

    [Header("informaçao sobre a pontuaçao")]
    public Text BestScoreInfo_text;

    public string normalText;
    public string newHighScoreText;

	void OnEnable()
    {
        CheckBestScore();
    }

    void CheckBestScore()
    {

        TimerCrud crud = new TimerCrud();
        int current = (int)GameObject.Find("_GC").GetComponent<_GC>().currentTime;

        bool high = crud.SetHighScore(current);
        //grava o novo highScore, se ele for maior, retorna true ou seja, é um novo score

        if (high)
        //caso nao seja um novo score
        {
            this.BestScoreValue_text.color = Color.yellow;
            this.BestScoreInfo_text.text = this.newHighScoreText;

        } else
        {
            this.BestScoreValue_text.color = Color.white;
            this.BestScoreInfo_text.text = this.normalText;
        }

        this.currentScore_text.text = UpdateTimeText((float)current);
        this.BestScoreValue_text.text = UpdateTimeText((float)crud.GetHighScore());

    }
    string UpdateTimeText(float newTime)
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

        return clockFormat;
        //atualiza o texto

    }
}
