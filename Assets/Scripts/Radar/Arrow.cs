using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour

/*Script desenvolvido por Rafael Pereira Santos contato: E-mail rafael.santos238@fatec.sp.gov.br
 * o uso desse script ou parte dele é permitido por mim para todo e qualquer fim, contato que 
 * esses creditos permaneçam no topo do script.
 * 
 */

{

    public GameObject target;
    Transform arrow;
    public float maxDistance = 0, minDistance;

    SpriteRenderer icone;

    // Use this for initialization
    void Start ()
    {
        this.icone = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        this.arrow = this.transform.GetChild(0);
        this.arrow.position = new Vector3(this.minDistance,0,0);

    }
	
	// Update is called once per frame
	void Update () {

        SelfDestroy();

        Rotate2Target();

        Positione();

        Fading();

	}

    void Fading()
    {
        if (target == null)
        {
            return;
        }

        float distance = Vector3.Distance(this.transform.parent.transform.position, this.target.transform.position);

        float color = this.maxDistance / distance;

        if(color > 1)
        {
            color = 1;
        }

        if(distance < this.minDistance)
        {
            color = 0;
        }        

        Color temp = this.icone.color;
        temp.a = color;
        this.icone.color = temp;


    }

    void Rotate2Target()
    {
        if(target == null)
        {
            return;
        }

        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 100);
    }

    void Positione()
    {
        if (target == null)
        {
            return;
        }

        this.gameObject.transform.position = this.transform.parent.transform.position;
    }

    void SelfDestroy()
    {
        if(this.target == null)
        {
            Destroy(this.gameObject);
        }
    }
}
