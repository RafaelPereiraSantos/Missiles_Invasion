using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour {

    public GameObject body;
    //o corpo do missel

    public float speed;
    public float rotationSpeed = 1;
    public GameObject target;

    Rigidbody2D missileRb;

    // Use this for initialization
    void Start()
    {
        missileRb = this.gameObject.GetComponent<Rigidbody2D>();

        FindTarget("player");
    }

    void Update()
    {
        if (GameObject.Find("_GC").GetComponent<_GC>().isPaused)
        {
            missileRb.velocity = transform.up * 0;
            this.missileRb.angularVelocity = 0;
            return;
        }

        GoFoward();

        Rotation();

    }

    void GoFoward()
    {
        if(this.missileRb == null)
        {
            return;
        }
        this.missileRb.velocity = transform.up * this.speed;
    }

    void Rotation()
    {
        if (this.body == null)
        //se o corpo nao existir, o o objeto propelido nao deve se mover
        {
            this.speed = 0;
            GoFoward();
        }
        if (target == null)
        {
            FindTarget("player");
            this.missileRb.angularVelocity = 0;
            return;
        }
        

        Vector2 point2Target = (Vector2)transform.position - (Vector2)this.target.transform.position;

        point2Target.Normalize();

        float value = Vector3.Cross(point2Target, transform.up).z;


        if (value > 0)
        {
            this.missileRb.angularVelocity = rotationSpeed;
        }
        else if (value < 0)
        {
            this.missileRb.angularVelocity = -rotationSpeed;
        }
    }


    void FindTarget(string targetTag)
    {
        if (GameObject.FindGameObjectWithTag(targetTag))
        {
            this.target = GameObject.FindGameObjectWithTag(targetTag);
        }
        else
        {
            //SelfDestroy();
        }
    }

    void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
