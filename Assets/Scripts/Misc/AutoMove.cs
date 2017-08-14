using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour

/*Script desenvolvido por Rafael Pereira Santos contato: E-mail rafael.santos238@fatec.sp.gov.br
 * o uso desse script ou parte dele é permitido por mim para todo e qualquer fim, contato que 
 * esses creditos permaneçam no topo do script.
 * 
 */

{

    public float speed;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.GetComponent<Rigidbody2D>().velocity = transform.up * this.speed;


    }
}
