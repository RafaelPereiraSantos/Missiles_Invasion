using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour {

    [Header("Velocidade extra")]

    public float extraX = 1;
    public float extraY = 1;

    MeshRenderer render;
    Material material;

	// Use this for initialization
	void Start ()
    {
        this.render = GetComponent<MeshRenderer>();
        this.material = render.material;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 offset = material.mainTextureOffset;

        offset.x = (transform.position.x/this.transform.lossyScale.x) * this.extraX;
        offset.y = (transform.position.y/ this.transform.lossyScale.y) * this.extraY;

        material.mainTextureOffset = offset;
	}
}
