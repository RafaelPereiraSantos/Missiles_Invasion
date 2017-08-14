using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextControll : MonoBehaviour {

    public static FloatingText popupText;
    private static GameObject canvas;

    public void Start()
    {
        Initialize();
    }

    public static void Initialize()
    {
        popupText = Resources.Load<FloatingText>("TextPopParent");
        //pega o texto direto na pasta
        canvas = GameObject.Find("Canvas");
    }

    public static void CreateFloatingText(string text, Transform position)
    {
        FloatingText instance = Instantiate(popupText);
        Vector2 screenpos = Camera.main.WorldToScreenPoint (position.position);
        /*pega a posiçao do objeto 2d e coloca para a screen position, isso só funciona se
         * no Canvas o "render mode" for "screen space"
        */
        instance.transform.SetParent(canvas.transform,false);
        instance.transform.position = new Vector2(position.position.x, position.position.y+0.1f);
        instance.SetText(text);

    }
}
