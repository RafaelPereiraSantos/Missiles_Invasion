using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowards : MonoBehaviour {

    [Header("Alvo a ser seguido")]
    [Tooltip("Alvo que o objeto que possui esse scripte ira seguir")]
    public Transform Target;

    [Header("Velocidade de giro")]
    [Tooltip("caso a velocidade seja 0, ira seguir o target sem delay")]
    public float turnSpeed;

    [Header("Eixtos")]
    public bool axisX = true;
    public bool axisY = false;
    public bool axisZ = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(this.Target == null)
        {
            return;
        }

        TurnAxis();
		
	}

    void TurnAxis()
    {
        Vector3 direction = (this.Target.position - this.transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        if (!this.axisX)
        {
            lookRotation.x = 0;
        }

        if (!this.axisY)
        {
            lookRotation.y = 0;
        }

        if (!this.axisZ)
        {
            lookRotation.z = 0;
        }

        if (this.turnSpeed == 0)
            //caso a velocidade seja zero, o giro segue o alvo perfeitamente
        {
            this.transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * Mathf.Infinity);
        } else
        {

            this.transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * this.turnSpeed);
        }


    }
}
