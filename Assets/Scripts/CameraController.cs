using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    /*Script desenvolvido por Rafael Pereira Santos contato: E-mail rafael.santos238@fatec.sp.gov.br
     * o uso desse script ou parte dele é permitido por mim para todo e qualquer fim, contato que 
     * esses creditos permaneçam no topo do script.
     * 
     */

	public GameObject 		player;

	public float  velocity;

    [Header("limites do cenario")]

    public Transform limitLeft;
    public Transform limitRight;
    public Transform limitTop;
    public Transform limitButton;

	public	bool useLerp;

    float posX, posY;

	
	public void _Start () {

        FindPlayer();
	
	}
	
	void Update ()
    {
        if (player == null)
        {
            if (GameObject.Find("_GC").GetComponent<_GC>().isOnPlaying) {
                FindPlayer();
            }
            return;
        }
        else
        {
            StopAllCoroutines();

            posX = player.transform.position.x;
            posY = player.transform.position.y;
        }

        if (useLerp == false) {
            //se o lerp estiver desligado, a camera seguira o player igualmente, caso contrario
            //havera um delay em relaçao a camera com o personagem

			if ((posX > limitLeft.position.x && posX < limitRight.position.x) &&(posY > limitButton.position.y && posY < limitTop.position.y)) {			
				transform.position = new Vector3 (posX, posY, transform.position.z);
			}

		} else { 
            if ((posX > limitLeft.position.x && posX < limitRight.position.x) 
                && (posY > limitButton.position.y && posY < limitTop.position.y)) {		
                //limita o quanto a camera por de mover em relaçao ao mapa eixo x a esquerda
				follow ();
			} 
		    if ((transform.position.x > limitLeft.position.x && transform.position.x < limitRight.position.x)
                && (transform.position.y > limitButton.position.y && transform.position.y < limitTop.position.y))
            {
                //limita o quanto a camera por de mover em relaçao ao mapa eixo x a direita
                follow();
			}
        }	
    }
        //METODOS

	void follow(){
        //metodo para a camera seguir o player com um certo delay
		Vector3 destino = new Vector3 (posX, posY, transform.position.z);
		transform.position = Vector3.Lerp (transform.position, destino, velocity * Time.deltaTime);
	}

    void FindPlayer()
    {
        this.player = GameObject.Find("player");
    }
    
    public void _Go2Start()
    {
        transform.position = new Vector3(0, 0, -10);
    }

    IEnumerator PosToStart()
    {
        yield return new WaitForSeconds(3);
        posY = posX = 0;
    }

}

