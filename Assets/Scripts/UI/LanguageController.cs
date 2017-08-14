using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageController : MonoBehaviour {

    /*Script desenvolvido por Rafael Pereira Santos contato: E-mail rafael.santos238@fatec.sp.gov.br
     * o uso desse script ou parte dele é permitido por mim para todo e qualquer fim, contato que 
     * esses creditos permaneçam no topo do script.
     * 
     */
    public bool startLanguage;
    public int language2Start;

    public Text languageValue;
    public string[] languagesAvaliables;
    int actualId;

	// Use this for initialization
	void Start ()
    {
        if (this.startLanguage)
        {
            if(this.language2Start != PlayerPrefs.GetInt("lang"))
            {
                PlayerPrefs.SetInt("lang", language2Start);
            }
        }


        this.actualId = PlayerPrefs.GetInt("lang");
        
        _SetMainLanguage(0);

    }

    public void _SetMainLanguage(int i)
        //com o comando vindo do botao a lingua escolhida vai uma casa adiante no array de linguas
        //disponiveis
    {
        this.actualId += i;

        if(this.actualId > this.languagesAvaliables.Length - 1)
            //caso passe da ultima lingua no array, volta para o primeiro
        {
            this.actualId = 0;
        }

        PlayerPrefs.SetInt("lang", this.actualId);

        if(this.languageValue != null)
        {
            this.languageValue.text = languagesAvaliables[this.actualId];
        }       

        _ChangeAllTextsOnScreen();
    }
	
	void _ChangeAllTextsOnScreen()
    {
        Text[] allTexts = FindObjectsOfType<Text>();
        //encontra todos os textos na tela
        foreach(Text texto in allTexts)
            //envia uma mensagem em cada texto da tela para mudar a linguagem
        {
            texto.transform.SendMessage("ChangeText", SendMessageOptions.DontRequireReceiver);
        }
    }
}
