using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour {     

    public side rotDirection;

    public enum side
    {
        left, right, up, down,notRotation
    }

    public float rotSpeed, moveToSpeed;
    Vector3 rotation,stayPos;

    void Start()
    {
        rot();
    }

    // Update is called once per frame
    void Update () {

        this.transform.Rotate(this.rotation.x, this.rotation.y, this.rotation.z);

        if(this.transform.position != this.stayPos)
        {
            MoveToStayPos();
        }

    }

    void rot()
        //decide que lado ele ira girar
    {
        if(this.rotDirection == side.left)
        {
            this.rotation = new Vector3(0,1,0);
        } else if (this.rotDirection == side.right)
        {
            this.rotation = new Vector3(0,-1,0);
        }
        else if (this.rotDirection == side.up)
        {
            this.rotation = new Vector3(-1,0,0);
        }
        else if (this.rotDirection == side.down)
        {
            this.rotation = new Vector3(1,0,0);
        } else
        {
            this.rotation = new Vector3(0, 0, 0);
        }
    }

    void NewPos (Transform newPos)
    {
        this.stayPos = newPos.position;
    }

    void MoveToStayPos()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, this.stayPos, this.moveToSpeed);
    }
}
