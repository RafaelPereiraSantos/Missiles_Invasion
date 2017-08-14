using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Space(2)]
    [Header("caracteristicas")]
    public float speedMax;
    public float speed;
    public float turnSpeed;

    Rigidbody2D playerRb;

	// Use this for initialization
	void Start ()
    {
        this.playerRb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(this.speed < this.speedMax)
        {
            this.speed += 0.1f;
        }

        if (GameObject.Find("_GC").GetComponent<_GC>().isPaused || !GameObject.Find("_GC").GetComponent<_GC>().isOnPlaying)
        {
            playerRb.velocity = transform.up * 0 ;
            this.playerRb.angularVelocity = 0;
            return;
        }

        float h = 0;

        

#if UNITY_EDITOR
        h = Input.GetAxis("Horizontal");            
#endif

            if (Input.touchCount > 0)
        //primeiro toque
        {
            Vector3 test = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            Touch[] touchs = Input.touches;

            if (touchs[0].position.x < Screen.width / 2)
            //left
            {
                h = -1;
            }
            else if (touchs[0].position.x > Screen.width / 2)
            //right
            {
                h = 1;
            }
        }

        Turn(h);
        GoFoward();
    }

    void Turn(float side)
    {
        float torque = 0;

        if(side < 0)
        {
            torque = this.turnSpeed;
        } else if (side > 0)
        {
            torque = this.turnSpeed * -1;
        } else
        {
            torque = 0;
        }

        this.playerRb.angularVelocity = torque;
    }

    void GoFoward()
    {
        playerRb.velocity = transform.up * this.speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.transform.tag != "Collectable")
        {
            if (!GameObject.Find("_GC").GetComponent<_GC>().isPaused)
            {

                GameObject.Find("Canvas").GetComponent<MenuController>().ChangeMenu("LoseMenu");
                GameObject.Find("_GC").GetComponent<_GC>()._GameState();

                //save time

            }
        }
    }
}
