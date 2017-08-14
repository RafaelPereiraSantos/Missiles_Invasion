using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    /*Script desenvolvido por Rafael Pereira Santos contato: E-mail rafael.santos238@fatec.sp.gov.br
     * o uso desse script ou parte dele é permitido por mim para todo e qualquer fim, contato que 
     * esses creditos permaneçam no topo do script.
     * 
     */

    public GameObject[] Menus;//lista com todos os menus
    public string FirstMenuOn;//primeiro menu a ser aberto

    /*scripte que controla qual menu estará visivel
     */

    void OnEnable()
    {
        int i = 0;

        /*foreach(GameObject menu in GameObject.FindGameObjectsWithTag("menu"))
            //pega todos os menus e joga em um array
        {
            this.Menus[i] = menu;
            i++;
        }*/

        ChangeMenu(FirstMenuOn);

    }

    public void ChangeMenu(string menuSelected)
        //desliga todos os menus com excessao do escolhido
    {
        foreach(GameObject menu in this.Menus)
        {            
            if(menu.transform.name == menuSelected)
            {
                menu.SetActive(true);
            } else
            {
                menu.SetActive(false);
            }
        }
    }
}
