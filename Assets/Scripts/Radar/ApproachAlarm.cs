using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachAlarm : MonoBehaviour {

    //detecta se exite algum "inimigo" na area indicada

    public int maxErrorAngle;
    public string enemies;
    public bool playAlarm;
    int enemysOnAlarmArea;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == enemies) //&& 
            //Mathf.Abs(Mathf.Abs(col.transform.eulerAngles.z) - Mathf.Abs(this.transform.parent.transform.eulerAngles.z )) < this.maxErrorAngle) 
        {
            this.enemysOnAlarmArea += 1;
        }
        Alarm();
    }

    void OnDestroy()
    {
        GameObject.Find("_SFX").GetComponent<AudioManager>().Stop("Alarm");
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == enemies)
        {
            this.enemysOnAlarmArea -= 1;
        }
        Alarm();
    }

    void Alarm()
    {
        if (!GameObject.Find("_SFX").GetComponent<AudioManager>().IsPlaying("Alarm") && this.enemysOnAlarmArea > 0)
        {
            GameObject.Find("_SFX").GetComponent<AudioManager>().Play("Alarm");
        }
        else if (GameObject.Find("_SFX").GetComponent<AudioManager>().IsPlaying("Alarm") && this.enemysOnAlarmArea <= 0)
        {
            GameObject.Find("_SFX").GetComponent<AudioManager>().Stop("Alarm");
        }
    }
}
