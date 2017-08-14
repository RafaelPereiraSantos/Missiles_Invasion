using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Swipe : MonoBehaviour

/*Script desenvolvido por Rafael Pereira Santos contato: E-mail rafael.santos238@fatec.sp.gov.br
 * o uso desse script ou parte dele é permitido por mim para todo e qualquer fim, contato que 
 * esses creditos permaneçam no topo do script.
 * 
 */

{

    public int minDragDistance;

    float dragDistance;

    float dragSpeed;

    public bool working;

    Vector3 firstPos;
    Vector3 endPos;

    

    void Start()
    {
        this.dragDistance = Screen.height * this.minDragDistance / 100;
        this.working = true;
    }
    
    public string Drag()
    {
        string dragSide = "null";

        if (!this.working)
        {
            return dragSide;
        }

        if (Input.touchCount == 0) { this.dragSpeed = 0; return dragSide; }

        if (Input.touchCount == 1)
            //trabalha apenas com um toque
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                //detecta o primeiro toque
            {
                this.firstPos = touch.position;
                this.endPos = touch.position;
            } else if (touch.phase == TouchPhase.Moved)
            {
                this.endPos = touch.position;
            } else if (touch.phase == TouchPhase.Ended) 
                //fim do toque
            {
                this.endPos = touch.position;

                if(Mathf.Abs(endPos.x - firstPos.x) > Mathf.Abs(endPos.y - firstPos.y))
                    //detecta se é vertical ou horizontal
                {
                    //horizontal
                    if(endPos.x > firstPos.x)
                    {
                        //DIREITA
                        dragSide = "right";

                        
                    } else
                    {
                        //ESQUERDA
                        dragSide = "left";
                    }

                    this.dragSpeed = Mathf.Abs(endPos.x - firstPos.x);

                } else
                {
                    if (endPos.y > firstPos.y)
                    {
                        //CIMA
                        dragSide = "up";
                    }
                    else
                    {
                        //BAIXO
                        dragSide = "down";
                    }

                    this.dragSpeed = Mathf.Abs(endPos.y - firstPos.y);
                }
            }
        } else
        {
            //APENAS UM TOQUE
            dragSide = "tap";
        }

        return dragSide;
    }

    public float DragSpeed()
        //retorna a distancia do drag
    {
        return this.dragSpeed;
    }
}
