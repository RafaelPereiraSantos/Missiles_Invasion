using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioCrud : MonoBehaviour {

    //salva as preferencias do usuario se ele quer ou não ouvir sons
    
    public Image[] soundBtnImg;
    public Text[] soundText;
    
    public Sprite onSprite;
    public Sprite offSprite;

    public string onText;
    public string offText;

    // Use this for initialization
    void Start ()
    {
        _UpdateValues();
	}

    public void _OnOff()
    {
        int p = 0;
        if(PlayerPrefs.GetInt("sound") == 0)
        {
            p = 1;
        }

        PlayerPrefs.SetInt("sound",p);

        _UpdateValues();
    }

    public void _UpdateValues()
    {
        if(PlayerPrefs.GetInt("sound") == 1)
        {
            SetOn();
            Debug.Log("turn sound on");
        } else
        {
            SetOff();
            Debug.Log("turn sound off");
        }
    }

    void SetOn()
    {
        if(soundText.Length > 0)
        {
            foreach(Text text in soundText)
            {
                ChangeText(this.onText, text);
            }
        }

        if (soundBtnImg.Length > 0)
        {
            foreach (Image img in soundBtnImg)
            {
                ChangeSprite(this.onSprite, img);
            }
        }

        if (GameObject.Find("_GC").GetComponent<_GC>().isPaused)
            //se o jogo estiver pausado, não desmuta 
        {
            return;
        }
        _MuteAll(false);
    }

    void SetOff()
    {
        if (soundText.Length > 0)
        {
            foreach (Text text in soundText)
            {
                ChangeText(this.offText, text);
            }
        }

        if (soundBtnImg.Length > 0)
        {
            foreach (Image img in soundBtnImg)
            {
                ChangeSprite(this.offSprite, img);
            }
        }

        _MuteAll(true);
    }

    public void _MuteAll(bool mute)
    {
        GameObject[] all = FindObjectsOfType<GameObject>();
        //pega todos os objetos do jogo

        foreach (GameObject obj in all)
        {
            if (obj.GetComponent<AudioSourceManager>() != null)
                //se o objeto possuir um Componente de audio, obviamente tera um controle de som proprio
            {
                    obj.GetComponent<AudioSourceManager>()._Update();                
            }
        }
    }

    void ChangeText(string newText, Text text2Change)
    {
        text2Change.text = newText;
    }

    void ChangeSprite(Sprite newSprite, Image image2Change)
    {
        image2Change.sprite = newSprite;
    }
}
