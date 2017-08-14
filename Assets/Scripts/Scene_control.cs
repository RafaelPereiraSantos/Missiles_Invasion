using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_control : MonoBehaviour {

    /*Script desenvolvido por Rafael Pereira Santos contato: E-mail rafael.santos238@fatec.sp.gov.br
     * o uso desse script ou parte dele é permitido por mim para todo e qualquer fim, contato que 
     * esses creditos permaneçam no topo do script.
     * 
     */

    private AsyncOperation async = null;

    public GameObject timeBar_front,timeBarObject;
    public Text BarProgress_text;

    public bool autoChange = false;
    public string sceneToAutoChange;

    public float extraTime;

    Vector3 scale;

    void Start()
    {
        if (this.autoChange)
        {
            _ChangeSceneTo(sceneToAutoChange);
        }
    }

    public void _ChangeSceneTo(string SceneName)
    {

        StartCoroutine(LoadRoutine(SceneName));

    }

    private IEnumerator LoadRoutine(string SceneName)
    {
        //inicia o carregamento da proxima cena
        this.async = SceneManager.LoadSceneAsync(SceneName);
        this.async.allowSceneActivation = false;

        ChangeBarScale(0);

        yield return new WaitForSeconds(this.extraTime);

        while (async.progress < 0.9f)
        {
            ChangeBarScale(async.progress);
            yield return null;
        }

        ChangeBarScale(1);
        yield return new WaitForSeconds(this.extraTime);
        this.async.allowSceneActivation = true;
    }

    void ChangeBarScale(float scaleX)
    {
        if(this.timeBarObject == null)
            //caso a barra não exita, não faz nada
        {
            return;
        }

        if(this.timeBarObject.active == false)
            //ativa a barra pega a escala atual
        {
            this.timeBarObject.SetActive(true);
            scale = this.timeBar_front.transform.localScale;
        }

        scale.x = scaleX;
        this.timeBar_front.transform.localScale = scale;

        BarProgress_text.text = ((int)scaleX*100).ToString() + "%";
    }
}
