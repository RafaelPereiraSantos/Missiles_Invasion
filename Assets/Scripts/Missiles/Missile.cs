using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    public ParticleSystem smoke;

    void OnDestroy()
    {
        if(this.smoke == null)
        {
            return;
        }

        this.smoke.loop = false;       

        Destroy(this.gameObject.transform.parent.gameObject, this.smoke.startLifetime);
    }
}
