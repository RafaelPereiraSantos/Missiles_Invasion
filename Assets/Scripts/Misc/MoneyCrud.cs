using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCrud : MonoBehaviour {

    public Text playerMoney_text;

    public float playerMoney;

    void Start()
    {       
        changeMoneyValue();

        ChangeText();
    }

    void changeMoneyValue()
    {
        this.playerMoney = _GetMoney();
    }

	public float _GetMoney()
        //retorna quanto o player possui
    {
        float moneyInBank = PlayerPrefs.GetFloat("playermoney");

        return moneyInBank;
    }

    public void _SetMoney(float money)
        //altera quanto o player possui
    {
        Debug.Log("debito" + money);

        float moneyInBank = PlayerPrefs.GetFloat("playermoney");

        moneyInBank += money;

        moneyInBank = Mathf.Round(moneyInBank);

        PlayerPrefs.SetFloat("playermoney", moneyInBank);

        changeMoneyValue();

        ChangeText();        
    }

    void ChangeText()
    {
        if (GameObject.Find("playerMoney_text") != null)
        {
            this.playerMoney_text = GameObject.Find("playerMoney_text").GetComponent<Text>();
        }

        if (this.playerMoney_text != null)
        {
            this.playerMoney_text.text = "$" + playerMoney.ToString();
        }
    }    
}

//testes
public class Testes : MonoBehaviour
{
    
    static MoneyCrud crud = new MoneyCrud();

    //[UnityEditor.MenuItem("Tools/Money/Add")]
    public static void AddMoney()
    {    
        crud._SetMoney(500);
    }

    //[UnityEditor.MenuItem("Tools/Money/Remove")]
    public static void RemoveMoney()
    {
        crud._SetMoney(-500);
    }

    //[UnityEditor.MenuItem("Tools/Prefs/ResetAll")]
    public static void ResetPreferns()
    {
        PlayerPrefs.DeleteAll();
    }

}
