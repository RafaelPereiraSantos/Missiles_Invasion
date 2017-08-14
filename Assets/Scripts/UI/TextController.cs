using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    /*Script desenvolvido por Rafael Pereira Santos contato: E-mail rafael.santos238@fatec.sp.gov.br
     * o uso desse script ou parte dele é permitido por mim para todo e qualquer fim, contato que 
     * esses creditos permaneçam no topo do script.
     * 
     */
    [Header("Ditar texto")]
    public bool DicteText;
    public float timeForLetter;

    [Space(4)]
    [Header("Portugues")]
    [TextArea(3, 5)]
    public string ptTextArea;

    [Header("Inglês")]
    [TextArea(3, 5)]
    public string engTextArea;

    // Use this for initialization
    /*void Start () {
        OnEnable();		
	}*/

    void OnEnable()
    {
        ChangeText();
    }

    void ChangeText()
    {
        int langId = PlayerPrefs.GetInt("lang"); //1 pt; 2 eng; 3 span
        Text thisText = this.gameObject.GetComponent<Text>();

        string text = "";

        switch (langId)
        {
            case 0: //pt
                {
                    text = this.ptTextArea;
                    break;
                }
            case 1: //eng
                {
                    text = this.engTextArea;
                    break;
                }
            case 2: //span
                {
                    break;
                }
        }

        if (this.DicteText)
        {
            StartCoroutine(Dictate(text));
        } else
        {
            thisText.text = text;
        }       

    }

    IEnumerator Dictate(string text)
        //esse metodo insere o texto, letra por letra
    {
        Text thisText = this.gameObject.GetComponent<Text>();

        int letra = 0;

        thisText.text = "";

        while (letra < text.Length)
        {
            thisText.text += text[letra];
            letra += 1;
            yield return new WaitForSeconds(this.timeForLetter);
        }

    }
}
