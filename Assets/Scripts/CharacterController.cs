using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {

    /*Script desenvolvido por Rafael Pereira Santos contato: E-mail rafael.santos238@fatec.sp.gov.br
     * o uso desse script ou parte dele é permitido por mim para todo e qualquer fim, contato que 
     * esses creditos permaneçam no topo do script.
     * 
     */

    [Header("Textos informativos")]
    public Text characterPrice_text;
    public Text characterSpeed_text;
    public Text characterTurnSpeed_text;
    public Text warning_text;

    [Header("botoes")]
    public Button buyButton;
    public Button BackButton;

    [Header("parent dos modelos")]
    public GameObject carModelsParent;

    public MoneyCrud moneyC;

    Swipe sw;

    [Header("Characters")]
    public Character[] character;
    public GameObject[] models;
    public GameObject nullCharacter;

    [Header("Extra Options")]
    public bool useNullCharacter = false;

    [Space(2)]
    [Header("Arrows")]
    public GameObject backArrow;
    public GameObject nextArrow;

    [Space(2)]
    [Header("Characters position")]
    public Transform  leftPos;
    public Transform  middlePos;
    public Transform  rightPos;

    //para o swipe
    Rect area;

    public int currentCharacter;

    void Start()
    {
        this.sw = new Swipe();
        float heigh = Screen.height;
        float width = Screen.width;
        SetArea(0,heigh/4*1,width,heigh/4*2);

        _SwipeOnOff(true);
    }

    void OnEnable()
    {
        UpdateEverthing();

    }

    public void SetArea(float xmin, float ymin, float xmax, float ymax)
    {
        //GUI.Box(new Rect(150, 150, 300, 300), "Loader Menu");
        this.area = new Rect(xmin, ymin, xmax, ymax);
    }

    void Update()
    {
        //Debug.Log(width);

        string side = "null";
        if (Input.touchCount > 0)
        {
            if (this.area.Contains(Input.GetTouch(0).position))
            {
                side = this.sw.Drag();
                Debug.Log("inside");
            }
        }

        switch (side)
            //funçao swipe
        {
            case "left":
                {
                    _NextCharacter(1);
                    break;
                }
            case "right":
                {
                    _NextCharacter(-1);
                    break;
                }
            default:
                {
                    break;
                }            
        }
    }

	// Use this for initialization
	void UpdateEverthing () {

        CreateCharacters();

        _PositionCharacters();

        AllowCharacters();

        _MoveCharacters();

        _NextCharacter(0);
        //Chama o "_NextCharacter com um valor nulo para que a seta esquerda inicie inativa

        ShowPrice();
        
        
    }

    public void _SwipeOnOff(bool state)
        //para/reinicia o funcionamento do swipe deste scripte
    {
        this.sw.working = state;
        Debug.Log("swipe:" + state);
    }

    public void _SelectCharacter()
        //define o veiculo que sera usado
    {
        SpawnPlayer spawnP = GameObject.Find("_GC").GetComponent<SpawnPlayer>();

        spawnP.characterId          = this.currentCharacter;
        spawnP.characterSpeed       = this.character[this.currentCharacter].speed;
        spawnP.characterTurnSpeed   = this.character[this.currentCharacter].turnSpeed;
    }

    public void _NextCharacter(int i)
        //seleciona um novo personagem
    {
        this.currentCharacter += i;        

        if(this.currentCharacter == 0)
            //desativa o botao esquerdo caso alcanse o limite 
        {
            this.backArrow.SetActive(false);
        } else
        {
            this.backArrow.SetActive(true);
        }

        if (this.currentCharacter >= this.character.Length -1)
        //desativa o botao direito caso alcanse o limite 
        {
            this.nextArrow.SetActive(false);
        } else
        {
            this.nextArrow.SetActive(true);
        }

        if (this.currentCharacter < 0)
        {
            this.currentCharacter = 0;
            return;
        }
        else if (this.currentCharacter > this.character.Length - 1)
        {
            this.currentCharacter = this.character.Length - 1;
            return;
        }

        if(i != 0)
            //só toca o som se for diferente de zero, porque no "Start" é enviado um comando 0 para "alinhar" os characters
        {
            GameObject.Find("_SFX").GetComponent<AudioManager>().Play("Wind");
            //toca o som de compra
        }

        //toca o som de compra

        _MoveCharacters();
        ShowPrice();
        ShowCharacterInfo();

    }

    public void _UnlockNewCharacter()
        //desbloqueia um novo personagem caso o dinheiro seja o suficiente
    {

        if(this.character[this.currentCharacter].price <= this.moneyC._GetMoney()) //SUBSTITUIS MONEY PELA FUNÇAO QUE PEGA O DINHEIRO DO JOGADOR
        {
            PlayerPrefs.SetInt(string.Format("character{0}", this.currentCharacter),1);
            //libera o veiculo
            this.moneyC._SetMoney(-this.character[this.currentCharacter].price);
            //retira da conta o valor do veiculo

            GameObject.Find("_SFX").GetComponent<AudioManager>().Play("Cash");
            //toca o som de compra

            UpdateEverthing();
        } else
        {
            //CASO NÃO HAJA DINHEIRO SUFICIENTE
            StartCoroutine(ChangeMessageForWhile());
        }
    }
	
	void AllowCharacters()
        //checa se cada personagem ja foi comprado, os que não foram comprados, teram sua skin substituida por algo irreconhecivel
    {
        for(int i = 0; i<= this.character.Length -1;i++)
        {
            if(this.character[i].price == 0)
                //caso o personagem seja gratis, libera ele
            {
                PlayerPrefs.SetInt(string.Format("character{0}", i), 1);
            }
            if (PlayerPrefs.GetInt(string.Format("character{0}", i.ToString())) == 1)
            //caso ele tenha sido comprado, atualiza o preço para zero o que futuramente ira remover o valor da tela            
            {
                this.character[i].price = 0;
            }    
        }
    }

    void CreateCharacters()
    {
        foreach(GameObject character in this.models)
        {
            if (character != null)
            {
                Destroy(character);
            }
        }

        int i = 0;
        foreach(Character character in this.character)
        {
            if (PlayerPrefs.GetInt(string.Format("character{0}", i.ToString())) == 0 && this.useNullCharacter)
            {
                this.models[i] = Instantiate(this.nullCharacter);                
            } else
            {
                this.models[i] = Instantiate(this.character[i].preFab);
            }

            this.models[i].transform.SetParent(this.carModelsParent.transform);
            this.models[i].transform.rotation = this.models[i].transform.parent.rotation;
            i++;
        }
    }

    public void _MoveCharacters()
    {
        int i = 0;
        foreach(GameObject character in this.models)
        {
            if (i < this.currentCharacter)
                //menor que o id atual, o personagem vai para a esquerda da lista
            {
                //character.transform.position = Vector3.Lerp(character.transform.position, this.leftPos.position,0.1f);// this.leftPos.position;
                character.SendMessage("NewPos", this.leftPos, SendMessageOptions.RequireReceiver);
            } else if (i > this.currentCharacter)
            //menor que o id atual, o personagem vai para a direita da lista
            {
                //character.transform.position = this.rightPos.position;
                character.SendMessage("NewPos", this.rightPos, SendMessageOptions.RequireReceiver);
            } else if (i == this.currentCharacter)
            {
                //character.transform.position = this.middlePos.position;
                character.SendMessage("NewPos", this.middlePos, SendMessageOptions.RequireReceiver);
            }
            i++;
        }

    }

    void ChangeButton(bool buy)
        //caso o personagem ainda estiver bloqueado e for selecionado o botao de "selecionar" muda para "comprar"
    {
        if (buy)
        {
            this.buyButton.gameObject.SetActive(true);
            this.BackButton.gameObject.SetActive(false);
        } else
        {
            this.buyButton.gameObject.SetActive(false);
            this.BackButton.gameObject.SetActive(true);
        }
    }

    void ShowPrice()
        //troca o preço na tela
    {
        if (this.character[this.currentCharacter].price <= 0)
        {
            this.characterPrice_text.text = "";
            ChangeButton(false);
            //muda o botao para selecionar uma vez que ja foi comprado
        }
        else
        {
            this.characterPrice_text.text = "$" + this.character[this.currentCharacter].price.ToString();
            ChangeButton(true);
            //muda o botao para comprar pois é necessario
        }
    }

    void ShowCharacterInfo()
    {
        this.characterSpeed_text.text = this.character[this.currentCharacter].speed.ToString();
        this.characterTurnSpeed_text.text = this.character[this.currentCharacter].turnSpeed.ToString();
    }

    public void _PositionCharacters()
    {
        foreach (GameObject character in this.models)
        {
            character.transform.position = this.middlePos.position;
        }

        int i = 0;
        foreach (GameObject character in this.models)
        {
            if (i < this.currentCharacter)
            //menor que o id atual, o personagem vai para a esquerda da lista
            {
                character.transform.position = this.leftPos.position;
            }
            else if (i > this.currentCharacter)
            //menor que o id atual, o personagem vai para a direita da lista
            {
                character.transform.position = this.rightPos.position;
            }
            else if (i == this.currentCharacter)
            {
                character.transform.position = this.middlePos.position;
            }
            i++;
        }
    }

    IEnumerator ChangeMessageForWhile()
        //metodo para mostrar uma mensagem rapida no lugar da outra
    {
        this.warning_text.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        this.warning_text.gameObject.SetActive(false);
    }
}

[System.Serializable]
public class Character
{
    public string name;

    public float speed;

    public float turnSpeed;

    public float price;

    public GameObject preFab;

}