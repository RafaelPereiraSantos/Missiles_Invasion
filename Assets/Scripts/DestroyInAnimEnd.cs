using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInAnimEnd : MonoBehaviour {
    public float delay;

    // Use this for initialization
    void Start()
    //destroi objeto ao fim da animaçao
    {
        Destroy(this.gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);

    }
}
