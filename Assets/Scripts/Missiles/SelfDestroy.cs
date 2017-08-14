using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour {

    public float lifeTime;

    Vector3 spawnPotion;

	// Use this for initialization
	void Start ()
    {
        this.spawnPotion = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameObject.Find("_GC").GetComponent<_GC>().isPaused)
        {
            return;
        }

        this.lifeTime -= Time.deltaTime;

		if(this.lifeTime <= 0)
        {
            Destroy();
        }
	}

    void Destroy()
    {
        this.GetComponent<Destroy>().Self();
    }
}
